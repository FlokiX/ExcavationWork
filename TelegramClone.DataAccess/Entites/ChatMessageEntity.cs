using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramClone.DataAccess.Entites
{
    public class ChatMessageEntity
    {
        public Guid Id { get; set; } 
        public Guid ChatId { get; set; } 
        public string SenderUsername { get; set; } 
        public string Content { get; set; } 
        public DateTime Timestamp { get; set; } = DateTime.UtcNow; 

        // Связь с чатом
        public ChatEntity Chat { get; set; } 
    }
}
