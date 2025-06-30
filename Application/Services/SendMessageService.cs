using EdgarBot.Application.Interfaces;

namespace EdgarBot.Application.Services;

public class SendMessageService(IMessageSender messageSender) : ISendMessageService
{
    private const string BanText = "Вы были заблокированы администраторами.\nУзнать новости фестиваля вы можете в канале @Nevermorequestions";

    public Task SendBannedInfoAsync(long userId, CancellationToken cancellationToken)
    {
        return messageSender.SendTextMessageAsync(userId, BanText, cancellationToken);
    }

    public Task SendMessageAsync(long chatId, string text, CancellationToken cancellationToken)
    {
        return messageSender.SendTextMessageAsync(chatId, text, cancellationToken);
    }
}
