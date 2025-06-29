using System.Collections.Concurrent;
using EdgarBot.Application.Interfaces;
using EdgarBot.Application.Models;

namespace EdgarBot.Infrastructure.Persistence;

public class InMemoryMappingStore : IMappingStore
{
    private readonly ConcurrentDictionary<int, ForwardedMessageInfo> _mapping = new();

    public void Add(int adminMessageId, ForwardedMessageInfo info)
    {
        _mapping[adminMessageId] = info;
    }

    public void Remove(int adminMessageId)
    {
        _mapping.TryRemove(adminMessageId, out _);
    }

    public bool TryGet(int adminMessageId, out ForwardedMessageInfo info)
    {
        return _mapping.TryGetValue(adminMessageId, out info);
    }
}