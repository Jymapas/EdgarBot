using EdgarBot.Application.Interfaces;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace EdgarBot.Presentation.Telegram;

public class UpdateHandler(IForwardingService forwardingService, IAdminReplyHandler adminReplyHandler, long adminChatId)
{
    public async Task HandleUpdateAsync(Update update, CancellationToken cancellationToken = default)
    {
        if (update.Type != UpdateType.Message || update.Message == null)
        {
            return;
        }

        var msg = update.Message;

        if (msg.Chat.Type == ChatType.Private)
        {
            if (msg.From?.IsBot == true)
            {
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
