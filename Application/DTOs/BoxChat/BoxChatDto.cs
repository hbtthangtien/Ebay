using Application.DTOs.Chatbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.BoxChat
{
    public class BoxChatDto
    {
        public int ProductId { get; set; }
        public int SellerId { get; set; }
        public string ProductTitle { get; set; } = "";
        public string SellerName { get; set; } = "";
        public string ProductImage { get; set; } = "";
        public int UnreadCount { get; set; }
        public int ReceiverId { get; set; }
        public List<MessageView> Messages { get; set; } = new();
    }
}
