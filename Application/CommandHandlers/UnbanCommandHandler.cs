using EdgarBot.Application.Interfaces;
using Telegram.Bot.Types;

namespace EdgarBot.Application.CommandHandlers;

public class UnbanCommandHandler(IBanListStore banListStore, ISendMessageService sendMessageService, IMappingStore mappingStore, long adminChatId) : ICommandHandler
{
    public async Task<bool> TryHandleAsync(Message message, CancellationToken cancellationToken)
    {
        var text = message.Text?.Trim();
        if (message.Chat.Id != adminChatId 
            || !(text.Equals("/unban", StringComparison.InvariantCultureIgnoreCase) 
                 || text.StartsWith("/unban@", StringComparison.InvariantCultureIgnoreCase)) 
            || message.ReplyToMessage == null)
        {
            return false;
        }

        if (mappingStore.TryGet(message.ReplyToMessage.MessageId, out var mappedMessage))
        {
            var userId = mappedMessage.UserId;
            var userName = mappedMessage.UserName;
            banListStore.Unban(userId);
            await sendMessageService.SendMessageAsync(adminChatId, $"Пользователь {userName} разбанен.", cancellationToken);
            return true;
        }

        await sendMessageService.SendMessageAsync(adminChatId, "Не удалось определить пользователя для разбана.", cancellationToken);
        return true;
    }
}
