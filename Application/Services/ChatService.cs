using Application.DTOs.BoxChat;
using Application.DTOs.Chatbox;
using Application.Extensions;
using Application.Interfaces.IServices;
using CoreLayer.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ChatService : IChatService
    {
        private readonly CloneEbayDbContext _context;
        public ChatService(CloneEbayDbContext cloneEbayDbContext)
        {
            _context = cloneEbayDbContext;
        }
        public async Task<List<BoxChatDto>> GetAllBoxChatsForUser(int userId)
        {
            var products = await _context.Messages
                .Where(e => e.SenderId == userId || e.ReceiverId == userId)
                .Select(e => new
                {
                    ProductId = e.ProductId,
                    ProductImage = e.Product.Images,
                    ProductTitle = e.Product.Title,
                    SellerId = e.Product.SellerId,
                    SellerName = e.Product.Seller.Username

                })
                .Distinct() 
                .ToListAsync();

            var data = new List<BoxChatDto>();
            foreach (var p in products)
            {
                var messages = await _context.Messages
                    .Where(m => (m.SenderId == userId || m.ReceiverId == userId) && m.ProductId == p.ProductId)
                    .ProjectToType<MessageView>()
                    .ToListAsync();

                data.Add(new BoxChatDto
                {
                    ProductId = p.ProductId ?? 0,
                    ProductImage = p.ProductImage,
                    ProductTitle = p.ProductTitle,
                    Messages = messages,
                    SellerId = (int)p.SellerId,
                    SellerName = p.SellerName
                    
                });
            }

            return data;

        }
        public async Task<BoxChatDto?> GetBoxChat(int userId, int productId, int sellerId)
        {
            // Lấy product + seller
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return null;

            // Lấy toàn bộ tin nhắn của Box
            var msgs = await _context.Messages
                                .Where(m => m.ProductId == productId && m.Product.SellerId == sellerId &&
                                           (m.SenderId == userId || m.ReceiverId == userId))
                                .OrderBy(m => m.Timestamp)
                                .Select(m => m.Adapt<MessageView>())
                                .ToListAsync();

            

            return new BoxChatDto
            {
                ProductId = productId,
                SellerId = sellerId,
                ProductTitle = product.Title,
                ProductImage = product.Images,
                SellerName = "Seller #" + sellerId,
                UnreadCount = 0,
                Messages = msgs
            };
        }

    }

}
