namespace EdgarBot.Application.Interfaces;

public interface IBanListStore
{
    void Ban(long userId);
    void Unban(long userId);
    bool IsBanned(long userId);
    IEnumerable<long> GetBannedUsers();
}
