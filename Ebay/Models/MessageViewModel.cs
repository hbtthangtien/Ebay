using Application.Extensions;

namespace Ebay.Models
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Content { get; set; } = "";
        public DateTime Timestamp { get; set; }
        public string DecryptMessage => EncryptMessage.Decrypt(Content);
    }
}
