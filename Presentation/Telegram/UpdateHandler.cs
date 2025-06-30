using EdgarBot.Application.Interfaces;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace EdgarBot.Presentation.Telegram;

public class UpdateHandler(
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

        var msg = update.Message;
        var user = msg.From;

        if (msg.Chat.Type == ChatType.Private)
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

            await forwardingService.HandleUserMessageAsync(msg, cancellationToken);
            return;
        }

        if (msg.Chat.Id == adminChatId && msg.ReplyToMessage != null)
        {
            await adminReplyHandler.HandleAdminReplyAsync(msg, cancellationToken);
        }
    }
}
