namespace TelegramClone.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TelegramClone.Application.Services; 
using TelegramClone.Core.Models;
using TelegramClone.Core.Abstractions.IService;


[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly IContactService _contactService; 

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    // Получение контакта по Gmail
    [HttpGet("byGmail")]
    public async Task<IActionResult> GetContactByGmail([FromQuery] string gmail)
    {
        if (string.IsNullOrWhiteSpace(gmail))
        {
            return BadRequest("Gmail is required.");
        }

        var contact = await _contactService.GetContactByGmailAsync(gmail);
        if (contact == null)
        {
            return NotFound("Contact not found.");
        }

        return Ok(contact);
    }
}
