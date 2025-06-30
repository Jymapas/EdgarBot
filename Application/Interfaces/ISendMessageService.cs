namespace EdgarBot.Application.Interfaces;

public interface ISendMessageService
{
    Task SendMessageAsync(
        long chatId,
        string text,
        CancellationToken cancellationToken = default);

    Task SendBannedInfoAsync(
        long userId,
        CancellationToken cancellationToken = default);
}
