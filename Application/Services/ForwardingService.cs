using EdgarBot.Application.Interfaces;
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

    public Task HandleUserMessageAsync(Message userMessage, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
