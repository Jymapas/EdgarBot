using EdgarBot.Application.Interfaces;
using EdgarBot.Application.Models;
using Microsoft.Data.Sqlite;

namespace EdgarBot.Infrastructure.Persistence;

public class SQLiteMappingStore : IMappingStore
{
    private readonly string _connectionString;

    public SQLiteMappingStore(string dbPath)
    {
        _connectionString = $"Data Source={dbPath}";
        EnsureTable();
    }

    public void Add(int adminMessageId, ForwardedMessageInfo info)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = """
                                INSERT OR REPLACE INTO ForwardedMessages
                                     (AdminMessageId, UserId, UserName, UserMessageId, ForwardedAt)
                                VALUES (@adminMsgId, @userId, @userName, @userMsgId, @fwdAt);
                              """;
        command.Parameters.AddWithValue("@adminMsgId", adminMessageId);
        command.Parameters.AddWithValue("@userId", info.UserId);
        command.Parameters.AddWithValue("@userName", info.UserName ?? string.Empty);
        command.Parameters.AddWithValue("@userMsgId", info.UserMessageId);
        command.Parameters.AddWithValue("@fwdAt", info.ForwardedAt.ToString("o"));
        command.ExecuteNonQuery();
    }

    public bool TryGet(int adminMessageId, out ForwardedMessageInfo info)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT UserId, UserName, UserMessageId, ForwardedAt FROM ForwardedMessages WHERE AdminMessageId = @id;";
        command.Parameters.AddWithValue("@id", adminMessageId);
        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            info = new ForwardedMessageInfo
            {
                AdminMessageId = adminMessageId,
                UserId = reader.GetInt64(0),
                UserName = reader.GetString(1),
                UserMessageId = reader.GetInt32(2),
                ForwardedAt = DateTime.TryParse(reader.GetString(3), out var dt) ? dt : DateTime.UtcNow
            };
            return true;
        }
        info = null;
        return false;
    }

    public void Remove(int adminMessageId)
    {
        throw new NotImplementedException();
    }

    private void EnsureTable()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = """
                              CREATE TABLE IF NOT EXISTS ForwardedMessages (
                                  AdminMessageId INTEGER PRIMARY KEY,
                                  UserId INTEGER NOT NULL,
                                  UserName TEXT,
                                  UserMessageId INTEGER,
                                  ForwardedAt TEXT
                              );
                              """;
        command.ExecuteNonQuery();
    }
}
