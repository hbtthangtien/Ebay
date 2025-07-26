using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Ebay.Controllers
{
    [Route("store")]
    public class StoreController : Controller
    {
        private readonly IStorePopupService _popupSvc;

        public StoreController(IStorePopupService popupSvc)
            => _popupSvc = popupSvc;

        [HttpGet("popup")]
        public async Task<IActionResult> Popup(
       int sellerId,
       int? productId,
         int? originId,
       int page = 1,
       int pageSize = 10)
        {
            var vm = await _popupSvc.GetStorePopupAsync(sellerId, productId, page, pageSize);

            if (vm is null) return NotFound();
            ViewBag.SellerId = sellerId;   // <-- thêm
            ViewBag.ProductId = originId ?? productId;

            return PartialView("_StorePopup", vm);

        }
    }
}
