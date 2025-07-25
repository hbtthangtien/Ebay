using Application.DTOs;
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
        public async Task<IReadOnlyList<HomeProductDTO>> GetListProductsForHomepage(int max = 7)
        
             => await _db.Products
                    .AsNoTracking()
                    .OrderByDescending(p => p.Id)
                    .Take(max)
                    .ProjectToType<HomeProductDTO>()   // *** zero-config ***
                    .ToListAsync();
        
    }
}
