using Application.DTOs.Chatbox;
using Application.Extensions;
using Application.Interfaces.IServices;
using CoreLayer.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly CloneEbayDbContext _context;
        public MessageService(CloneEbayDbContext cloneEbayDbContext)
        {
            _context = cloneEbayDbContext;
        }
        public void SaveMessage(MessageDto dto)
        {
            var message = dto.Adapt<Message>();
            message.Content = EncryptMessage.Encrypt(dto.Content);
            message.Timestamp = DateTime.Now;
            _context.Add(message);
            _context.SaveChanges();
        }
    }
}
