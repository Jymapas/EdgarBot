using EdgarBot.Application.Interfaces;
using EdgarBot.Application.Models;
using Microsoft.Data.Sqlite;

namespace EdgarBot.Infrastructure.Persistence;

public class SQLiteBanListStore : IBanListStore
{
    public void Ban(long userId, string name)
    {
        throw new NotImplementedException();
    }
    public void Unban(long userId)
    {
        throw new NotImplementedException();
    }
    public bool IsBanned(long userId)
    {
        throw new NotImplementedException();
    }
    public IEnumerable<BannedUserInfo> GetBannedUsers()
    {
        throw new NotImplementedException();
    }
}
