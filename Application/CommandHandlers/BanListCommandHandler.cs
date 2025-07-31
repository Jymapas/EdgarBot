using System.Text;
using EdgarBot.Application.Helpers;
using EdgarBot.Application.Interfaces;
using Telegram.Bot.Types;

namespace EdgarBot.Application.CommandHandlers;

public class BanListCommandHandler(IBanListStore banListStore, ISendMessageService sendMessageService, IMappingStore mappingStore, long adminChatId) : ICommandHandler
{
    public async Task<bool> TryHandleAsync(Message message, CancellationToken cancellationToken)
    {
        var text = message.Text?.Trim();
        if (message.Chat.Id != adminChatId 
            || !CommandHelper.IsCommand(text, "banlist")
            || message.ReplyToMessage == null)
        {
            return false;
        }
        
        var bannedUsers = banListStore.GetBannedUsers();

        if (!bannedUsers.Any())
        {
            await sendMessageService.SendMessageAsync(adminChatId, "Нет забаненных пользователей.", cancellationToken);
            return true;
        }

        var sb = new StringBuilder();
        foreach (var user in bannedUsers)
        {
            sb.AppendLine($"{user.Name} (`{user.UserId}`)");
        }
        
        await sendMessageService.SendMessageAsync(adminChatId, sb.ToString(), cancellationToken);
        
        return true;
    }
}