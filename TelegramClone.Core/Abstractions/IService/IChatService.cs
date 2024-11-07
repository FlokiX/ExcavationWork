using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramClone.Core.Models;

namespace TelegramClone.Core.Abstractions.IService
{
    public interface IChatService
    {
        Task<Chat> CreateOrGetChatAsync(Guid currentUserId, Guid contactUserId, Guid contactId);
        Task<List<Guid>> GetUserIdsInChatAsync(Guid chatId);
        Task<(ChatMessage message, string error)> SendMessageAsync(Guid chatId, Guid senderId, string senderUsername, string content, DateTime? timestamp);
    }
}
