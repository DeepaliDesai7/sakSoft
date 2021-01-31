using System;
using System.Collections.Generic;
using System.Text;

namespace WholesaleProductsSolution
{
    public class Product
    {
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public string ProductDescription { get; set; }
        
        readonly public List<Product> MockProducts;

        //public Product()
        //{
        //    MockProducts = new List<Product>() {
        //        new Product(){ProductId=1,ProductName="Mouse",ProductDescription="peripheral devices"},
        //        new Product(){ProductId=2,ProductName="Pendrive"},
        //        new Product(){ProductId=3,ProductName="Data Cabel",ProductDescription="requires cabel"},
        //        new Product(){ProductId=4,ProductName="Headphone",ProductDescription="requires cabel"},
        //        new Product(){ProductId=5,ProductName="KeyBoard",ProductDescription="peripheral devices"}
        //    };

        //}
        //public List<Product> GetAllProducts()
        //{
        //    return MockProducts;
        //}
    }
}
