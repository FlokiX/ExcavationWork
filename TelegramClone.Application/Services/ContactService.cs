using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using TelegramClone.Core.Models;
using TelegramClone.Core.Abstractions.IRepository;
using TelegramClone.Core.Abstractions.IService;
namespace TelegramClone.Application.Services;


public class ContactService : IContactService
{
    private readonly IContactRepository _contactRepository;

    public ContactService(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

   
    public async Task<Contact> GetContactByGmailAsync(string gmail)
    {
        return await _contactRepository.GetContactByGmailAsync(gmail);
    }
}
