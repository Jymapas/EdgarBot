using Telegram.Bot.Types.ReplyMarkups;

namespace EdgarBot.Application.Interfaces;

public interface ISendMessageService
{
    Task SendMessageAsync(
        long chatId,
        string text,
        CancellationToken cancellationToken = default);

    Task SendMessageAsync(
        long chatId,
        string text,
        ReplyMarkup? replyMarkup = null,
        CancellationToken cancellationToken = default);

    Task SendBannedInfoAsync(
        long userId,
        CancellationToken cancellationToken = default);
}
