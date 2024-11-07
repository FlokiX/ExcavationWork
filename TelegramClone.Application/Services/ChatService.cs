using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramClone.Core.Abstractions.IRepository;
using TelegramClone.Core.Abstractions.IService;
using TelegramClone.Core.Models;
using TelegramClone.DataAccess.Entites;

namespace TelegramClone.Application.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IContactRepository _contactRepository; 

        public ChatService(IChatRepository chatRepository, IContactRepository contactRepository)
        {
            _chatRepository = chatRepository;
            _contactRepository = contactRepository; 
        }

        
        public async Task<Chat> CreateOrGetChatAsync(Guid currentUserId, Guid contactUserId, Guid contactId)
        {
            
            var chat = await _chatRepository.CreateChatAsync(currentUserId, contactUserId, contactId);

           
            await _contactRepository.UpdateContactChatIdAsync(contactId, chat.Id);

            return chat;
        }

     
        public async Task<IEnumerable<ChatMessage>> GetChatMessagesAsync(Guid chatId)
        {
            var messages = await _chatRepository.GetMessagesAsync(chatId);
            return messages;
        }


        public async Task<List<Guid>> GetUserIdsInChatAsync(Guid chatId)
        {
            return await _chatRepository.GetUserIdsInChatAsync(chatId);
        }

        /*// Отправка сообщения в чат
        public async Task<(ChatMessage message, string error)> SendMessageAsync(Guid chatId, string senderUsername, string content)
        {
            
            var chat = await _chatRepository.GetChatByIdAsync(chatId);
            if (chat == null)
            {
                return (null, "Chat not found.");
            }

            // Отправка сообщения
            var (message, error) = chat.SendMessage(senderUsername, content);
            if (error != null)
            {
                return (null, error); // Возврат ошибки при создании сообщения
            }

            await _chatRepository.SaveMessageAsync(message);

            return (message, null); // Возвращаем отправленное сообщение
        }*/

        public async Task<(ChatMessage message, string error)> SendMessageAsync(Guid chatId, Guid senderId, string senderUsername, string content, DateTime? timestamp)
        {
            // Получение чата
            var chat = await _chatRepository.GetChatByIdAsync(chatId);
            if (chat == null)
            {
                return (null, "Chat not found.");
            }

            var message = ChatMessage.FromExisting(Guid.NewGuid(), chatId, senderUsername, content, timestamp ?? DateTime.UtcNow);

          
            await _chatRepository.SaveMessageAsync(message);

            return (message, null);


            // отправка сообщения в сервер а он россылает сообщения подключеным пользователмя отображая в чате

        }


    }
}
