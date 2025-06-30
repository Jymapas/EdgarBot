using EdgarBot.Application.Models;

namespace EdgarBot.Application.Interfaces;

public interface IBanListStore
{
    void Ban(long userId, string name);
    void Unban(long userId);
    bool IsBanned(long userId);
    IEnumerable<BannedUserInfo> GetBannedUsers();
}
