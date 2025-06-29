using EdgarBot.Application.Interfaces;
using EdgarBot.Application.Models;

namespace EdgarBot.Infrastructure.Persistence;

public class InMemoryMappingStore : IMappingStore
{
    public void Add(int adminMessageId, ForwardedMessageInfo info)
    {
        throw new NotImplementedException();
    }

    public void Remove(int adminMessageId)
    {
        throw new NotImplementedException();
    }

    public bool TryGet(int adminMessageId, out ForwardedMessageInfo info)
    {
        throw new NotImplementedException();
    }
}