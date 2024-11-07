using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelegramClone.Core.Abstractions.IRepository;
using TelegramClone.Core.Models;
using TelegramClone.DataAccess.Entites;

namespace TelegramClone.DataAccess.Repository
{
    public class ChatRepository : IChatRepository
    {
        private readonly ApplicationDbContext _context;

        public ChatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Chat> CreateChatAsync(Guid currentUserId, Guid contactUserId, Guid contactId)
        {
            // Проверка на существующий чат между этими пользователями
            var existingChat = await _context.Chats
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(c => (c.User1Id == currentUserId && c.User2Id == contactUserId) ||
                                          (c.User1Id == contactUserId && c.User2Id == currentUserId));

            if (existingChat != null)
            {
                // Если чат уже существует, вернуть его вместе с сообщениями
                var messages = existingChat.Messages
                    .Select(m => ChatMessage.FromExisting(m.Id, m.ChatId, m.SenderUsername, m.Content, m.Timestamp))
                    .ToList();
                return Chat.FromExisting(existingChat.Id, existingChat.User1Id, existingChat.User2Id, messages);
            }

            // Создание нового чата, если его не существует
            var (newChat, error) = Chat.Create(currentUserId, contactUserId);
            if (error != null)
            {
                throw new InvalidOperationException(error);
            }


            var chatEntity = MapToChatEntity(newChat);
            _context.Chats.Add(chatEntity);
            await _context.SaveChangesAsync();

            return newChat;
        }


        public async Task<IEnumerable<ChatMessage>> GetMessagesAsync(Guid chatId)
        {
            // Загрузка и сортировка сообщений для определенного чата
            var messages = await _context.ChatMessages
                .Where(m => m.ChatId == chatId)
                .OrderBy(m => m.Timestamp)
                .Select(m => ChatMessage.FromExisting(m.Id, m.ChatId, m.SenderUsername, m.Content, m.Timestamp))
                .ToListAsync();

            return messages;
        }

        private ChatEntity MapToChatEntity(Chat chat)
        {
            return new ChatEntity
            {
                Id = chat.Id,
                User1Id = chat.User1Id,
                User2Id = chat.User2Id,
                Messages = chat.Messages.Select(m => new ChatMessageEntity
                {
                    Id = m.Id,
                    ChatId = m.ChatId,
                    SenderUsername = m.SenderUsername,
                    Content = m.Content,
                    Timestamp = m.Timestamp
                }).ToList()
            };
        }


        public async Task<Chat> GetChatByIdAsync(Guid chatId)
        {

            var chatEntity = await _context.Chats
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(c => c.Id == chatId);

            if (chatEntity == null)
            {
                return null;
            }


            return Chat.FromExisting(
                chatEntity.Id,
                chatEntity.User1Id,
                chatEntity.User2Id,
                chatEntity.Messages.Select(m =>
                    ChatMessage.FromExisting(
                        m.Id,
                        m.ChatId,
                        m.SenderUsername,
                        m.Content,
                        m.Timestamp
                    )).ToList()
            );
        }

        public async Task SaveMessageAsync(ChatMessage message)
        {
            var messageEntity = new ChatMessageEntity
            {
                Id = message.Id,
                ChatId = message.ChatId,
                SenderUsername = message.SenderUsername,
                Content = message.Content,
                Timestamp = message.Timestamp
            };

            _context.ChatMessages.Add(messageEntity);
            await _context.SaveChangesAsync();
        }


        public async Task<List<Guid>> GetUserIdsInChatAsync(Guid chatId)
        {
            // Получаем чат по ID и возвращаем список ID пользователей
            var chat = await _context.Chats
                .Where(c => c.Id == chatId)
                .Select(c => new { c.User1Id, c.User2Id })
                .FirstOrDefaultAsync();

            if (chat != null)
            {
                return new List<Guid> { chat.User1Id, chat.User2Id };
            }

            return new List<Guid>(); // Возвращаем пустой список, если чат не найден
        }

    }
}

