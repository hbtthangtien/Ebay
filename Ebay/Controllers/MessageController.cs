using Application.DTOs.Chatbox;
using Application.Extensions;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ebay.Controllers
{
    [Route("api/messages")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IChatService _chatService;
        public MessageController(IMessageService messageService, IChatService chatService)
        {
            _messageService = messageService;
            _chatService = chatService;

        }

        [HttpPost]
        public IActionResult SaveMessage(MessageDto dto)
        {
            _messageService.SaveMessage(dto);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetChat(MessageDto dto)
        {
            var data = await _chatService.GetAllBoxChatsForUser(1);
            return Ok(data);
        }
    }
}
