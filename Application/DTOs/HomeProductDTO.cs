﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class HomeProductDTO
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public decimal? Price { get; set; }

        public string? Images { get; set; }
    }
}
