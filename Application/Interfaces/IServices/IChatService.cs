using Application.DTOs.BoxChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface IChatService
    {
        Task<List<BoxChatDto>> GetAllBoxChatsForUser(int userId);
        Task<BoxChatDto> GetBoxChat(int userId, int productId, int sellerId);
    }
}
