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
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _context;

        public ContactRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Contact> CreateContactAsync(Contact contact)
        {
       
            var entity = new ContactEntity
            {
                Id = contact.Id,
                UserId = contact.UserId,
                ContactUsername = contact.ContactUsername,
                ChatId = null 
            };

            _context.Set<ContactEntity>().Add(entity);
            await _context.SaveChangesAsync();

            return contact;
        }

        public async Task<Contact> GetContactByGmailAsync(string gmail)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == gmail);
            if (user == null) return null; 

            var contactEntity = await _context.Contacts.FirstOrDefaultAsync(c => c.UserId == user.Id);
            return MapToContact(contactEntity); 
        }

        public async Task UpdateContactChatIdAsync(Guid contactId, Guid chatId)
        {
            var contactEntity = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == contactId);

            if (contactEntity != null)
            {
                contactEntity.ChatId = chatId;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Контакт не найден.");
            }
        }

        // Метод преобразования ContactEntity в Contact
        private Contact MapToContact(ContactEntity contactEntity)
        {
            if (contactEntity == null) return null; 

           
            var contact = Contact.FromExisting(
                contactEntity.Id,            
                contactEntity.UserId,         
                contactEntity.ContactUsername, 
                contactEntity.ChatId ?? Guid.Empty
            );

            return contact; 
        }

    }

}
