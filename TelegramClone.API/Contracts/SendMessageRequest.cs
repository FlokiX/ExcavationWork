namespace TelegramClone.API.Contracts
{
    public class SendMessageRequest
    {
        public Guid ChatId { get; set; }          
        public Guid SenderId { get; set; }        
        public string SenderUsername { get; set; } 
        public string MessageText { get; set; }    
        public DateTime? Timestamp { get; set; }   
    }

}
