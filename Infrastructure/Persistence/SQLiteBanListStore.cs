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
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM BannedUsers WHERE UserId = @id;";
        command.Parameters.AddWithValue("@id", userId);
        command.ExecuteNonQuery();
    }

    public bool IsBanned(long userId)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT 1 FROM BannedUsers WHERE UserId = @id LIMIT 1;";
        command.Parameters.AddWithValue("@id", userId);
        using var reader = command.ExecuteReader();
        return reader.Read();
    }

    public IEnumerable<BannedUserInfo> GetBannedUsers()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT UserId, Name FROM BannedUsers;";
        using var reader = command.ExecuteReader();
        while (reader.Read())
            yield return new BannedUserInfo
            {
                UserId = reader.GetInt64(0),
                Name = reader.GetString(1),
            };
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
}
