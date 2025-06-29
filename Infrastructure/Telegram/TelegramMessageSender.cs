using EdgarBot.Application.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace EdgarBot.Infrastructure.Telegram;

public class TelegramMessageSender(ITelegramBotClient botClient) : IMessageSender
{
    public async Task<int> SendTextMessageAsync(long chatId, string text, ReplyParameters? replyParameters = null, CancellationToken cancellationToken = default)
    {
        var msg = await botClient.SendMessage(
            chatId,
            text,
            replyParameters: replyParameters,
            parseMode: ParseMode.Markdown,
            cancellationToken: cancellationToken);

        return msg.MessageId;
    }

    public async Task<int> CopyMessageAsync(long toChatId, long fromChatId, int messageId, CancellationToken cancellationToken = default)
    {
        var msg = await botClient.CopyMessage(
            toChatId,
            fromChatId,
            messageId,
            cancellationToken: cancellationToken);

        return msg;
    }

    public async Task<int> ForwardMessageAsync(long toChatId, long fromChatId, int messageId, CancellationToken cancellationToken = default)
    {
        var msg = await botClient.ForwardMessage(
            toChatId,
            fromChatId,
            messageId,
            cancellationToken: cancellationToken);

        return msg.MessageId;
    }

    public async Task<int> SendPhotoAsync(long chatId, string fileId, string? caption, CancellationToken cancellationToken = default)
    {
        var msg = await botClient.SendPhoto(
            chatId,
            fileId,
            caption,
            cancellationToken: cancellationToken);

        return msg.MessageId;
    }
}
