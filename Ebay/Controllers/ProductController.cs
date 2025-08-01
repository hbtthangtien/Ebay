﻿using Application.Interfaces.IServices;
using CoreLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Ebay.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productSvc;
        public ProductController(IProductService productSvc) => _productSvc = productSvc;



        [HttpGet("/Product/{id:int}")]
        public async Task<IActionResult> Detail(int id)
        {


            var vm = await _productSvc.GetDetailProduct(id);
            ViewBag.RatingStats = await _productSvc.GetProductRatingStatsAsync(id);
            return vm is null ? NotFound() : View(vm); 
        }
    }
}
