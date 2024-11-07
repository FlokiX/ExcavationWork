using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramClone.Core.Models;

namespace TelegramClone.Core.Abstractions.IRepository
{
    public interface IChatRepository
    {

        Task<Chat> CreateChatAsync(Guid currentUserId, Guid contactUserId, Guid contactId);
        Task<IEnumerable<ChatMessage>> GetMessagesAsync(Guid chatId);
        Task<Chat> GetChatByIdAsync(Guid chatId);
        Task SaveMessageAsync(ChatMessage message);

        Task<List<Guid>> GetUserIdsInChatAsync(Guid chatId);
    }

}
