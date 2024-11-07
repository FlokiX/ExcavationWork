using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramClone.Core.Models;

namespace TelegramClone.Core.Abstractions.IService
{
    public interface IContactService
    {
        Task<Contact> GetContactByGmailAsync(string gmail);
    }
}
