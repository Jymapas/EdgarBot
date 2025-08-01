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

        var user = userMessage.From;
        var userName = $"{user.FirstName} {user.LastName}".Trim();

        var mapping = new ForwardedMessageInfo
        {
            AdminMessageId = adminMessageId,
            UserId = user.Id,
            UserName = userName,
            UserMessageId = userMessage.MessageId,
            ForwardedAt = DateTime.UtcNow,
        };

        mappingStore.Add(adminMessageId, mapping);
    }
}
