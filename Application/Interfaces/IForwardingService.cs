using Telegram.Bot.Types;

namespace EdgarBot.Application.Interfaces;

public interface IForwardingService
{
    Task HandleUserMessageAsync(
        Message userMessage,
        CancellationToken cancellationToken = default);
}
