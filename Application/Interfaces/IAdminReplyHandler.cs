using Telegram.Bot.Types;

namespace EdgarBot.Application.Interfaces;

public interface IAdminReplyHandler
{
    Task HandleAdminReplyAsync(
        Message adminMessage,
        CancellationToken cancellationToken = default);
}
