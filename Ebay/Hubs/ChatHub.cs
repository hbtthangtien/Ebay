using Application.DTOs.Chatbox;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.SignalR;

namespace Ebay.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMessageService _messageService;

        public ChatHub(IMessageService messageService)
        {
            _messageService = messageService;
        }
        public async Task JoinBox(int productId, int userId, int receiverId)
        {
    
            int u1 = Math.Min(userId, receiverId);
            int u2 = Math.Max(userId, receiverId);
            string group = $"box_{productId}_{u1}_{u2}";
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        public async Task LeaveBox(int productId, int userId, int receiverId)
        {
            int u1 = Math.Min(userId, receiverId);
            int u2 = Math.Max(userId, receiverId);
            string group = $"box_{productId}_{u1}_{u2}";
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
        }

        public async Task SendMessage(MessageDto dto)
        {
            _messageService.SaveMessage(dto);

            string group = $"box_{dto.ProductId}_{Math.Min(dto.SenderId, dto.ReceiverId)}_{Math.Max(dto.SenderId, dto.ReceiverId)}";
  
            await Clients.Group(group).SendAsync("ReceiveMessage", new
            {
                dto.ProductId,
                dto.SenderId,
                dto.ReceiverId,
                dto.Content,
                timestamp = DateTime.Now
            });

        }
    }
}
