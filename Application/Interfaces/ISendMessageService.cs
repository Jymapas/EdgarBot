namespace EdgarBot.Application.Interfaces;

public interface ISendMessageService
{
    Task SendBannedInfoAsync(
        long userId,
        CancellationToken cancellationToken = default);
}
