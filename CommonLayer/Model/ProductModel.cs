using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class ProductModel
    {
        public string ProductName { get; set; }
        public string Description { get; set; }

        //(userId as FK)
        public int AdminId { get; set; }
        public string Image { get; set; }
        public string Rating { get; set; }
        public int Price { get; set; }
    }

    public class ProductModel2
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Rating { get; set; }
        public int Price { get; set; }
        //(userId as FK)
        public int AdminId { get; set; }
    }

    public class ProductModel3
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Rating { get; set; }
        public int Price { get; set; }
    }
}
