using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class StorePopupDto
    {
        // Store info
        public int Id { get; set; }
        public string? StoreName { get; set; }
        public string? BannerImageUrl { get; set; }
        public string? Description { get; set; }

        // Seller info
        public string? Username { get; set; }
        public string? AvatarUrl { get; set; }



        public int? TotalReviews { get; set; }
        public decimal? AverageRating { get; set; }
        public decimal? PositiveRate { get; set; }

     
        public List<ReviewDto> Reviews { get; set; }
        public int ReviewTotal { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public bool IsThisItemTab { get; set; }
    }
}
