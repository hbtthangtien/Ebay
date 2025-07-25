using Application.Interfaces.IServices;
using Ebay.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Ebay.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _svc;
        public HomeController(IProductService svc) => _svc = svc;

        public async Task<IActionResult> Index()
        {
            var products = await _svc.GetListProductsForHomepage();
            return View(products);
        }
    }
}
