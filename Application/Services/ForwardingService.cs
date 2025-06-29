using EdgarBot.Application.Interfaces;
using EdgarBot.Application.Models;
using Telegram.Bot.Types;

namespace EdgarBot.Application.Services;

public class ForwardingService(IMessageSender messageSender, IMappingStore mappingStore, long adminChatId) : IForwardingService
{
    public async Task HandleUserMessageAsync(Message userMessage, CancellationToken cancellationToken = default)
    {
        var adminMessageId = await messageSender.ForwardMessageAsync(
            adminChatId,
            userMessage.Chat.Id,
            userMessage.MessageId,
            cancellationToken);

        var mapping = new ForwardedMessageInfo
        {
            AdminMessageId = adminMessageId,
            UserId = userMessage.From.Id,
            UserMessageId = userMessage.MessageId,
        };

        mappingStore.Add(adminMessageId, mapping);
    }
}
