using EdgarBot.Application.Interfaces;
using EdgarBot.Application.Models;
using Microsoft.Data.Sqlite;

namespace EdgarBot.Infrastructure.Persistence;

public class SQLiteBanListStore : IBanListStore
{
    private readonly string _connectionString;

    public SQLiteBanListStore(string dbPath)
    {
        _connectionString = $"Data Source={dbPath}";
        EnsureTable();
    }

    private void EnsureTable()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = """
                              CREATE TABLE IF NOT EXISTS BannedUsers (
                                  UserId INTEGER PRIMARY KEY,
                                  Name TEXT
                              );
                              """;
        command.ExecuteNonQuery();
    }
    public void Ban(long userId, string name)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "INSERT OR REPLACE INTO BannedUsers (UserId, Name) VALUES (@id, @name);";
        command.Parameters.AddWithValue("@id", userId);
        command.Parameters.AddWithValue("@name", name ?? string.Empty);
        command.ExecuteNonQuery();
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
