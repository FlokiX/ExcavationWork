using Microsoft.AspNetCore.Mvc;
using TelegramClone.API.Contracts;
using TelegramClone.Application.Services;
using TelegramClone.Core.Abstractions.IService;
namespace TelegramClone.API.Controllers;




using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TelegramClone.API.Hubs; // Убедитесь, что у вас правильный импорт

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;
    private readonly IHubContext<ChatHub> _hubContext; // Добавляем IHubContext

    public ChatController(IChatService chatService, IHubContext<ChatHub> hubContext)
    {
        _chatService = chatService;
        _hubContext = hubContext; // Инициализируем IHubContext
    }


    [HttpPost("create")]
    public async Task<IActionResult> CreateChat([FromBody] CreateChatRequest request)
    {
        try
        {

            var chatId = await _chatService.CreateOrGetChatAsync(request.CurrentUserId, request.ContactUserId, request.ContactId);
            return Ok(new { ChatId = chatId });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("SendMessage")]
    public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest request)
    {
        if (request == null || request.ChatId == Guid.Empty || request.SenderId == Guid.Empty || string.IsNullOrWhiteSpace(request.MessageText))
        {
            return BadRequest("Invalid request data.");
        }

        try
        {
            // Сохранение сообщения через сервис
            await _chatService.SendMessageAsync(request.ChatId, request.SenderId, request.SenderUsername, request.MessageText, request.Timestamp);

            // Отправка сообщения через SignalR
            await _hubContext.Clients.Group(request.ChatId.ToString()).SendAsync("ReceiveMessage", request.SenderId, request.MessageText);

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while sending the message.");
        }
    }
}






