namespace EdgarBot.Application.Models;

public class ForwardedMessageInfo
{
    public int AdminMessageId { get; set; }
    public long UserId { get; set; }
    public string UserName { get; set; }
    public int UserMessageId { get; set; }
    public DateTime ForwardedAt { get; set; }
}
