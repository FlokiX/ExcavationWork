namespace TelegramClone.API.Contracts
{
    public class CreateChatRequest
    {
        public Guid CurrentUserId { get; set; } 
        public Guid ContactUserId { get; set; } 
        public Guid ContactId { get; set; }    
    }

}
