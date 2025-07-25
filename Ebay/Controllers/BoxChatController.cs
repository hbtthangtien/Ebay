using Application.Interfaces.IServices;
using CoreLayer.Entities;
using Ebay.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Ebay.Controllers
{
    public class BoxChatController : Controller
    {
        private readonly IChatService _chatService;
        public BoxChatController(IChatService chatService)
        {
            _chatService = chatService;
        }
        public async Task<IActionResult> Index(int? productId, int? sellerId)
        {

            int userId = 1; 

            if (productId.HasValue && sellerId.HasValue)
            {
                var box = await _chatService.GetBoxChat(userId, productId.Value, sellerId.Value);
                if (box == null) return NotFound();
                ViewBag.UserId = userId;
                var vm = box.Adapt<BoxChatViewModel>();
                return PartialView("_BoxChat", vm);      // _BoxChat.cshtml chứa #chat-box
            }

            // 3. Ngược lại: load tất cả box + chọn hội thoại đầu
            var boxes = await _chatService.GetAllBoxChatsForUser(userId);
            var model = boxes.Adapt<List<BoxChatViewModel>>();
            ViewBag.UserId = userId;

            return View(model);
        }
    }
}
