using EdgarBot.Application.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace EdgarBot.Infrastructure.Telegram;

public class TelegramMessageSender(ITelegramBotClient botClient) : IMessageSender
{
    public async Task<int> SendTextMessageAsync(long chatId, string text, CancellationToken cancellationToken = default)
    {
        var msg = await botClient.SendMessage(
            chatId,
            text,
            ParseMode.Markdown,
            cancellationToken: cancellationToken);

        return msg.MessageId;
    }

    public async Task<int> SendTextMessageAsync(long chatId, string text, ReplyMarkup? replyMarkup = null, CancellationToken cancellationToken = default)
    {
        var msg = await botClient.SendMessage(
            chatId,
            text,
            ParseMode.Markdown,
            replyMarkup: replyMarkup,
            cancellationToken: cancellationToken
        );
        
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
}
