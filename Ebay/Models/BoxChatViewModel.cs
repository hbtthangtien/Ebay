namespace Ebay.Models
{
    public class BoxChatViewModel
    {
        public int ProductId { get; set; }
        public int SellerId { get; set; }
        public string ProductTitle { get; set; } = "";
        public string SellerName { get; set; } = "";
        public string ProductImage { get; set; } = "";
        public int UnreadCount { get; set; }
        public List<MessageViewModel> Messages { get; set; } = new();
    }
}
