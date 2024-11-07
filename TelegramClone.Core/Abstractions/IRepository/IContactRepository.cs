using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramClone.Core.Models;

namespace TelegramClone.Core.Abstractions.IRepository
{
    public interface IContactRepository
    {
        //Task<Contact> GetContactByIdAsync(Guid contactId); 
        ///Task<IEnumerable<Contact>> GetContactsByUserIdAsync(Guid userId); 
        Task<Contact> CreateContactAsync(Contact contact); 
        Task UpdateContactChatIdAsync(Guid contactId, Guid chatId);
        Task<Contact> GetContactByGmailAsync(string gmail);
        //Task UpdateContactAsync(Contact contact); 
        //Task DeleteContactAsync(Guid contactId); 
    }

}
