using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ProductDetailDTO
    {
        public int Id { get; set; }

        public int SellerId { get; set; }
        public string? StoreName { get; set; }
        public string? Title { get; set; }
        public string? BannerImageUrl { get; set; }
        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public string? Images { get; set; }


    }
}
