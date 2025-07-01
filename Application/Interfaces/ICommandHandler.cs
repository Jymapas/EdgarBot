using Telegram.Bot.Types;

namespace EdgarBot.Application.Interfaces;

public interface ICommandHandler
{
    Task<bool> TryHandleAsync(Message message, CancellationToken cancellationToken);
}
