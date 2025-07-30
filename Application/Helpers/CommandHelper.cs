namespace EdgarBot.Application.Helpers;

public static class CommandHelper
{
    public static bool IsCommand(string? text, string command)
    {
        if (string.IsNullOrWhiteSpace(text)) return false;

        var messageText = text.Trim();

        if (!messageText.StartsWith('/')) return false;

        return messageText.StartsWith($"/{command}", StringComparison.InvariantCultureIgnoreCase)
               || messageText.StartsWith($"/{command}@", StringComparison.InvariantCultureIgnoreCase);
    }
}