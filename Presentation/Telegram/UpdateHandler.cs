using EdgarBot.Application.Interfaces;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace EdgarBot.Presentation.Telegram;

public class UpdateHandler(
    IForwardingService forwardingService,
    IAdminReplyHandler adminReplyHandler,
    IBanListStore banListStore,
    ISendMessageService sendMessageService,
    IMappingStore mappingStore,
    long adminChatId)
{
    public async Task HandleUpdateAsync(Update update, CancellationToken cancellationToken = default)
    {
        if (update.Type != UpdateType.Message || update.Message == null)
        {
            return;
        }

        var message = update.Message;
        var user = message.From;

        if (message.Chat.Type == ChatType.Private)
        {
            if (user.IsBot)
            {
                return;
            }

            if (banListStore.IsBanned(user.Id))
            {
                await sendMessageService.SendBannedInfoAsync(user.Id, cancellationToken);
                return;
            }

            await forwardingService.HandleUserMessageAsync(message, cancellationToken);
            return;
        }

        var chatId = message.Chat.Id;

        if (chatId == adminChatId && message.ReplyToMessage != null)
        {
            var messageText = string.IsNullOrWhiteSpace(message.Text)
                ? null
                : message.Text.Trim();
            if (messageText is not null && (messageText == "/ban" || messageText == "/unban"))
            {
                if (mappingStore.TryGet(message.ReplyToMessage.MessageId, out var mapping))
                {
                    var userId = mapping.UserId;
                    var userName = mapping.UserName ?? userId.ToString();

                    if (messageText == "/ban")
                    {
                        banListStore.Ban(userId, userName);
                        await sendMessageService.SendMessageAsync(chatId, $"Пользователь {userName} забанен.", cancellationToken);
                    }
                    else // "/unban"
                    {
                        banListStore.Unban(userId);
                        await sendMessageService.SendMessageAsync(chatId, $"Пользователь {userName} разбанен.", cancellationToken);
                    }
                }
                else
                {
                    await sendMessageService.SendMessageAsync(chatId, "Не удалось определить пользователя для бана/разбана.", cancellationToken);
                }
                return;
            }
            await adminReplyHandler.HandleAdminReplyAsync(message, cancellationToken);
        }
    }
}
