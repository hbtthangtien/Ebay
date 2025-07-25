using Application.DTOs;
using Application.Interfaces.IServices;
using CoreLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class StorePopupService : IStorePopupService
    {
        private readonly CloneEbayDbContext _ctx;

        public StorePopupService(CloneEbayDbContext ctx)
            => _ctx = ctx;
        public async Task<StorePopupDto?> GetStorePopupAsync(
         int sellerId,          // ← lấy trực tiếp sellerId
         int? productId,
         int page = 1,
         int pageSize = 10)
        {
  
            var store = await _ctx.Stores
                                  .Include(s => s.Seller)
                                  .FirstOrDefaultAsync(s => s.SellerId == sellerId);   

            if (store is null) return null;             

           
            var allReviewsQry = _ctx.Reviews
                                    .Include(r => r.Reviewer)
                                    .Include(r => r.Product)
                                    .Where(r => r.Product.SellerId == sellerId);

            // This-item vs All-items
            var tabReviewsQry = productId is null
                ? allReviewsQry
                : allReviewsQry.Where(r => r.ProductId == productId.Value);

            /*------------------------------------------------
             * 3) Thống kê
             *------------------------------------------------*/
            var totalReviews = await allReviewsQry.CountAsync();
            var avgRating = totalReviews == 0 ? 0
                                : await allReviewsQry.AverageAsync(r => r.Rating!.Value);

            var positiveCount = await allReviewsQry.CountAsync(r => r.Rating >= 3);
            var positiveRate = totalReviews == 0 ? 0
                                : (decimal)positiveCount / totalReviews * 100;

            /*------------------------------------------------
             * 4) Lấy danh sách review (paging)
             *------------------------------------------------*/
            var reviewTotal = await tabReviewsQry.CountAsync();
            var rawList = await tabReviewsQry
                            .OrderByDescending(r => r.CreatedAt)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .Select(r => new
                            {
                                r.Reviewer.Username,
                                r.Reviewer.AvatarUrl,
                                r.Comment,
                                r.Rating,
                                r.CreatedAt,
                                ProductTitle = r.Product.Title,
                                r.Product.Images    // giữ nguyên chuỗi
                            })
                            .ToListAsync();

            // 2) Map sang DTO ở bộ nhớ
            var reviewDtos = rawList.Select(x => new ReviewDto
            {
                Username = x.Username,
                AvatarUrl = x.AvatarUrl,
                Comment = x.Comment,
                Rating = x.Rating,
                CreatedAt = x.CreatedAt,
                Title = x.ProductTitle,
                Images = string.IsNullOrEmpty(x.Images)
                               ? null
                               : x.Images.Split(';')[0]      // OK vì giờ là LINQ to Objects
            }).ToList();


            return new StorePopupDto
            {
                // Store
                Id = store.Id,
                StoreName = store.StoreName,
                BannerImageUrl = store.BannerImageUrl,
                Description = store.Description,

                // Seller
                Username = store.Seller.Username,
                AvatarUrl = store.Seller.AvatarUrl,

                // Stats
                TotalReviews = totalReviews,
                AverageRating = Math.Round((decimal)avgRating, 2),
                PositiveRate = Math.Round(positiveRate, 1),

                // Reviews
                Reviews = reviewDtos,
                ReviewTotal = reviewTotal,
                CurrentPage = page,
                PageSize = pageSize,
                IsThisItemTab = productId.HasValue
            };
        }

    }
}
