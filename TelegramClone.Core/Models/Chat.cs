using System;
using System.Collections.Generic;

namespace TelegramClone.Core.Models
{
    public class Chat
    {
        public Guid Id { get; } // Уникальный идентификатор чата
        public Guid User1Id { get; } // ID первого пользователя
        public Guid User2Id { get; } // ID второго пользователя

        private List<ChatMessage> messages = new(); // Сообщения в чате
        public IReadOnlyList<ChatMessage> Messages => messages.AsReadOnly(); // Чтение сообщений

        
        private Chat(Guid id, Guid user1Id, Guid user2Id, List<ChatMessage> messages)
        {
            Id = id;
            User1Id = user1Id;
            User2Id = user2Id;
            this.messages = messages ?? new List<ChatMessage>();
        }

      
        public static (Chat chat, string error) Create(Guid user1Id, Guid user2Id)
        {
            if (user1Id == user2Id)
            {
                return (null, "Users cannot be the same."); 
            }

           
            var newChat = new Chat(Guid.NewGuid(), user1Id, user2Id, new List<ChatMessage>());
            return (newChat, null);
        }

        
        public static Chat FromExisting(Guid id, Guid user1Id, Guid user2Id, List<ChatMessage> messages)
        {
            return new Chat(id, user1Id, user2Id, messages);
        }

       
        public (ChatMessage message, string error) SendMessage(string senderUsername, string content)
        {
            var message = ChatMessage.Create(this.Id, senderUsername, content);

            if (message != null)
            {
                messages.Add(message); 
                return (message, null);
            }

            return (null, "Failed to create message.");
        }
    }
}
