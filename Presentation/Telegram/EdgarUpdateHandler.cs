using EdgarBot.Presentation;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

public class EdgarUpdateHandler(UpdateHandler handler) : IUpdateHandler
{
    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        await handler.HandleUpdateAsync(update, cancellationToken);
    }

    public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Polling error: {exception}");
        return Task.CompletedTask;
    }
}
