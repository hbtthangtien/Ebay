﻿using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface IProductService
    {
        Task<IReadOnlyList<HomeProductDTO>> GetListProductsForHomepage(int max = 7);

        Task<ProductDetailDTO> GetDetailProduct(int id);

        Task<ProductRatingStatsDTO> GetProductRatingStatsAsync(int productId);
    }
}
