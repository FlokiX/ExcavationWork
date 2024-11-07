using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramClone.DataAccess.Entites
{
    public class ChatEntity
    {
        public Guid Id { get; set; }
        public Guid User1Id { get; set; }
        public Guid User2Id { get; set; }
        public List<ChatMessageEntity> Messages { get; set; } = new();
    }
}
