using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramClone.DataAccess.Entites
{
    public class ContactEntity
    {
        public Guid Id { get; set; } 
        public Guid UserId { get; set; } 
        public string ContactUsername { get; set; } 

        // Связь один-к-одному с чатом
        public Guid? ChatId { get; set; } 
        public ChatEntity Chat { get; set; } 

        
        public UserEntity User { get; set; } 
    }
}
