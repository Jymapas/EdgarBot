using Telegram.Bot.Types;

namespace EdgarBot.Application.Interfaces;

public interface IMessageSender
{
    Task<int> SendTextMessageAsync(
        long chatId,
        string text,
        ReplyParameters? replyParameters = null,
        CancellationToken cancellationToken = default);

    Task<int> SendPhotoAsync(
        long chatId,
        string fileId,
        string? caption,
        CancellationToken cancellationToken = default);

    Task<int> CopyMessageAsync(
        long toChatId,
        long fromChatId,
        int messageId,
        CancellationToken cancellationToken = default);

    Task<int> ForwardMessageAsync(
        long toChatId,
        long fromChatId,
        int messageId,
        CancellationToken cancellationToken = default);
}
