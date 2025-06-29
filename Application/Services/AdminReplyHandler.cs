using EdgarBot.Application.Interfaces;
using Telegram.Bot.Types;

namespace EdgarBot.Application.Services;

public class AdminReplyHandler(IMessageSender sender, IMappingStore mappingStore) : IAdminReplyHandler
{
    public async Task HandleAdminReplyAsync(Message adminMessage, CancellationToken cancellationToken = default)
    {
        var reply = adminMessage.ReplyToMessage;
        if (reply == null)
        {
            return;
        }

        if (!mappingStore.TryGet(reply.MessageId, out var mapping))
        {
            return;
        }

        await sender.CopyMessageAsync(
            mapping.UserId,
            adminMessage.Chat.Id,
            adminMessage.MessageId,
            cancellationToken);
    }
}
