﻿using System;
using System.Collections.Generic;

namespace CoreLayer.Entities;

public partial class Category
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
