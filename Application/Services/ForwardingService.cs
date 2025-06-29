using EdgarBot.Application.Interfaces;
using EdgarBot.Application.Models;
using Telegram.Bot.Types;

namespace EdgarBot.Application.Services;

public class ForwardingService : IForwardingService
{
    private readonly long _adminChatId;
    private readonly IMappingStore _mappingStore;
    private readonly IMessageSender _sender;

    public ForwardingService(IMessageSender messageSender, IMappingStore mappingStore, long adminChatId)
    {
        _sender = messageSender;
        _mappingStore = mappingStore;
        _adminChatId = adminChatId;
    }

    public async Task HandleUserMessageAsync(Message userMessage, CancellationToken cancellationToken = default)
    {
        var adminMessageId = await _sender.ForwardMessageAsync(
            _adminChatId,
            userMessage.Chat.Id,
            userMessage.MessageId,
            cancellationToken);

        var mapping = new ForwardedMessageInfo
        {
            AdminMessageId = adminMessageId,
            UserId = userMessage.From.Id,
            UserMessageId = userMessage.MessageId,
        };

        _mappingStore.Add(adminMessageId, mapping);
    }
}
