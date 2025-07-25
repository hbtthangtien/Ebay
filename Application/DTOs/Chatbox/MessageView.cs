using Application.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Chatbox
{
    public class MessageView
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Content { get; set; } = "";
        public DateTime Timestamp { get; set; }

        public string DecryptMessage => EncryptMessage.Decrypt(Content);
    }
}
