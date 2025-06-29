using EdgarBot.Application.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace EdgarBot.Infrastructure.Telegram;

public class TelegramMessageSender : IMessageSender
{
    private readonly ITelegramBotClient _botClient;

    public TelegramMessageSender(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public async Task<int> SendTextMessageAsync(long chatId, string text, ReplyParameters? replyParameters = null, CancellationToken cancellationToken = default)
    {
        var msg = await _botClient.SendMessage(
            chatId: chatId,
            text: text,
            replyParameters: replyParameters,
            parseMode: ParseMode.Markdown,
            cancellationToken: cancellationToken);

        return msg.MessageId;
    }

    public async Task<int> CopyMessageAsync(long toChatId, long fromChatId, int messageId, CancellationToken cancellationToken = default)
    {
        var msg = await _botClient.CopyMessage(
            chatId: toChatId,
            fromChatId: fromChatId,
            messageId: messageId,
            cancellationToken: cancellationToken);

        return msg;
    }
}
