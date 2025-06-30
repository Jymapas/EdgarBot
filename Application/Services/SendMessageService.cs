using EdgarBot.Application.Interfaces;

namespace EdgarBot.Application.Services;

public class SendMessageService(IMessageSender messageSender) : ISendMessageService
{
    private const string Text = "Вы были заблокированы администраторами.\nУзнать новости фестиваля вы можете в канале @Nevermorequestions";

    public Task SendBannedInfoAsync(long userId, CancellationToken cancellationToken)
    {
        return messageSender.SendTextMessageAsync(userId, Text, cancellationToken);
    }
}
