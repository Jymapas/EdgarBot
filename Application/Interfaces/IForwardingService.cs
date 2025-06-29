namespace EdgarBot.Application.Interfaces;

public interface IForwardingService
{
    Task HandleUserMessageAsync(
        Telegram.Bot.Types.Message userMessage,
        CancellationToken cancellationToken = default);
}
