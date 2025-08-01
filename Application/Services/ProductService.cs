﻿using Application.DTOs;
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
    public class ProductService : IProductService
    {
        private readonly CloneEbayDbContext _db;
        public ProductService(CloneEbayDbContext db) => _db = db;

        public async Task<ProductDetailDTO> GetDetailProduct(int id)
        {
            var dto = await _db.Products
                    .AsNoTracking()
                    .Where(p => p.Id == id)
                    .ProjectToType<ProductDetailDTO>()    // Mapster sinh code, không cần cấu hình
                    .FirstOrDefaultAsync();

            if (dto is null) return null;

            // 2) Bổ sung thông tin Store (bảng Store – sellerId = product.SellerId)
            var store = await _db.Stores
                                  .AsNoTracking()
                                  .FirstOrDefaultAsync(s => s.SellerId == dto.SellerId);

            if (store is not null)
            {
                dto.StoreName = store.StoreName;
                dto.BannerImageUrl = store.BannerImageUrl;
                dto.Description = store.Description;   // nếu muốn mượn mô tả
            }

            return dto;
        }

        public async Task<IReadOnlyList<HomeProductDTO>> GetListProductsForHomepage(int max = 7)

             => await _db.Products
                    .AsNoTracking()
                    .OrderByDescending(p => p.Id)
                    .Take(max)
                    .ProjectToType<HomeProductDTO>()
                    .ToListAsync();



        public async Task<ProductRatingStatsDTO> GetProductRatingStatsAsync(int productId)
        {
            var q = _db.Reviews.Where(r => r.ProductId == productId && r.Rating != null);

            var total = await q.CountAsync();
            var avg = total == 0 ? 0 : await q.AverageAsync(r => r.Rating!.Value);

            var buckets = await q.GroupBy(r => r.Rating!.Value)
                                 .Select(g => new { g.Key, Cnt = g.Count() })
                                 .ToListAsync();

            var dto = new ProductRatingStatsDTO
            {
                TotalRatings = total,
                AverageRating = (decimal)avg
            };
            foreach (var b in buckets) dto.RatingCounts[b.Key] = b.Cnt;

            return dto;
        }
    }
}
