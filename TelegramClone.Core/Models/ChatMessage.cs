using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramClone.Core.Models
{
    public class ChatMessage
    {
        public Guid Id { get; } = Guid.NewGuid();
        public Guid ChatId { get; }
        public string SenderUsername { get; }
        public string Content { get; }
        public DateTime Timestamp { get; } = DateTime.UtcNow;

        private ChatMessage(Guid id, Guid chatId, string senderUsername, string content, DateTime timestamp)
        {
            Id = id;
            ChatId = chatId;
            SenderUsername = senderUsername;
            Content = content;
            Timestamp = timestamp;
        }

        
        public static ChatMessage Create(Guid chatId, string senderUsername, string content)
        {
            return new ChatMessage(
                Guid.NewGuid(),           
                chatId,
                senderUsername,
                content,
                DateTime.UtcNow          
            );
        }

        public static ChatMessage FromExisting(Guid id, Guid chatId, string senderUsername, string content, DateTime timestamp)
        {
            return new ChatMessage(id, chatId, senderUsername, content, timestamp);
        }
    }

}
