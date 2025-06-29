namespace EdgarBot.Application.Interfaces;

public interface IMessageSender
{
    Task<int> SendTextMessageAsync(long chatId, string text, int? replyToMessageId = null, CancellationToken cancellationToken = default);
    Task<int> CopyMessageAsync(long toChatId, long fromChatId, int messageId, CancellationToken cancellationToken = default);
}