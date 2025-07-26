using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ReviewDto
    {
        public string? Username { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Comment { get; set; }
        public int? Rating { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Title { get; set; }
        public string? Images { get; set; }
        
    }
}
