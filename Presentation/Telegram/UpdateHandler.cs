using EdgarBot.Application.Interfaces;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace EdgarBot.Presentation.Telegram;

public class UpdateHandler(
    IEnumerable<ICommandHandler> commandHandlers,
    IForwardingService forwardingService,
    IAdminReplyHandler adminReplyHandler,
    IBanListStore banListStore,
    ISendMessageService sendMessageService,
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
        
        if (!string.IsNullOrWhiteSpace(message.Text) && message.Text.StartsWith('/'))
        {
            foreach (var handler in commandHandlers)
            {
                if (await handler.TryHandleAsync(message, cancellationToken))
                {
                    return;
                }
            }
        }

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
            await adminReplyHandler.HandleAdminReplyAsync(message, cancellationToken);
        }
    }
}
