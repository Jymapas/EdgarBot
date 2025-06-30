using EdgarBot.Application.Models;

namespace EdgarBot.Application.Interfaces;

public interface IMappingStore
{
    void Add(int adminMessageId, ForwardedMessageInfo info);
    bool TryGet(int adminMessageId, out ForwardedMessageInfo info);
    void Remove(int adminMessageId);
}
