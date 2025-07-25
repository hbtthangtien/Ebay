using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface IStorePopupService
    {
        Task<StorePopupDto?> GetStorePopupAsync(
        int sellerId,
        int? productId,
        int page = 1,
        int pageSize = 10);
    }
}

