using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TelegramClone.Core.Abstractions.IService;
namespace TelegramClone.API.Hubs;



public class ChatHub : Hub { 

    private readonly IChatService _chatService;

    public ChatHub(IChatService chatService)
    {
        _chatService = chatService;
    }

    public async Task SendMessage(Guid chatId, string senderId, string messageText)
    {
        
        var userIds = await _chatService.GetUserIdsInChatAsync(chatId);

        foreach (var userId in userIds)
        {
           
            if (userId.ToString() != senderId)
            {
                await Clients.User(userId.ToString()).SendAsync("ReceiveMessage", senderId, messageText);
            }
        }
    }


}
