﻿using System;
using System.Collections.Generic;

#nullable disable

namespace DTOLibrary
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public bool Status { get; set; }
    }
}
