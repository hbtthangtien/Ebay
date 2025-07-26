using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ProductRatingStatsDTO
    {
        public int TotalRatings { get; set; }
        public decimal AverageRating { get; set; }        // 0-5
        public int[] RatingCounts { get; set; } = new int[6]; // index 1..5
    }
}
