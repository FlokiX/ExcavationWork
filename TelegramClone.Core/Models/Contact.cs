using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System;

namespace TelegramClone.Core.Models
{
    public class Contact
    {
        public Guid Id { get; } 
        public Guid UserId { get; }
        public string ContactUsername { get; } 
        public Guid? ChatId { get; }

        // Приватный конструктор
        private Contact(Guid id, Guid userId, string contactUsername, Guid chatId)
        {
            Id = id; 
            UserId = userId; 
            ContactUsername = contactUsername;
            ChatId = chatId; 
        }

        // Статический метод для создания нового контакта
        public static (Contact contact, string error) Create(Guid userId, string contactUsername)
        {
            if (string.IsNullOrWhiteSpace(contactUsername) || contactUsername.Length > 20)
            {
                return (null, "Contact username cannot be null or empty and must be less than or equal to 20 characters.");
            }

            var newContact = new Contact(Guid.NewGuid(), userId, contactUsername, Guid.Empty); 
            return (newContact, null);
        }

        // Статический метод для создания контакта из существующих данных
        public static Contact FromExisting(Guid id, Guid userId, string contactUsername, Guid chatId)
        {
            return new Contact(id, userId, contactUsername, chatId);
        }
    }
}
