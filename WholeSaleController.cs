using System;
using System.Collections.Generic;
using System.Linq;
using WholesaleUserssSolution;

namespace WholesaleProductsSolution
{
    public class WholesaleProductsSolution
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int Price { get; set; }

        public static List<Users> MockUsers;
        public static List<WholesaleProductsSolution> MockData;
        public static List<Product> MockProducts;


        //readonly List<WholesaleProductsSolution> MockData;

        public static void Main()
        {
            WholesaleProductsSolution wholesaleProductsSolution = new WholesaleProductsSolution();
            PrepareMockData(out MockData,out MockUsers,out MockProducts);
           //Console.WriteLine("{0}", wholesaleProductsSolution.GetOptimalPrice(1));
           //Console.WriteLine("{0}", wholesaleProductsSolution.GetTop10PurchasedProducts(DateTime.Now.AddDays(-7),DateTime.Now).ForEach(g));
           //Console.WriteLine("{0}", wholesaleProductsSolution.GetTop10UsersOnPurchaseValue(DateTime.Now.AddDays(-7), DateTime.Now));
           //Console.WriteLine("{0}", wholesaleProductsSolution.GetRelatedProducts(1));
            
            Console.ReadKey();
        }

        public static void PrepareMockData(out List<WholesaleProductsSolution> MockData,out List<Users> MockUsers, out List<Product> MockProducts)
        {
             MockData = new List<WholesaleProductsSolution>() {
                new WholesaleProductsSolution() {ProductId=1,UserId=1,PurchaseDate=DateTime.Now,Price=100 },
                new WholesaleProductsSolution() {ProductId=2,UserId=1,PurchaseDate=DateTime.Now.AddDays(-5),Price=300 },
                new WholesaleProductsSolution() {ProductId=3,UserId=1,PurchaseDate=DateTime.Now,Price=2000 },
                new WholesaleProductsSolution() {ProductId=4,UserId=1,PurchaseDate=DateTime.Now.AddDays(-5),Price=1000 },
                new WholesaleProductsSolution() {ProductId=5,UserId=1,PurchaseDate=DateTime.Now,Price=4000 },
                new WholesaleProductsSolution() {ProductId=1,UserId=2,PurchaseDate=DateTime.Now,Price=200 },
                new WholesaleProductsSolution() {ProductId=1,UserId=3,PurchaseDate=DateTime.Now.AddDays(-7),Price=150 },
                new WholesaleProductsSolution() {ProductId=2,UserId=4,PurchaseDate=DateTime.Now,Price=600 },
                new WholesaleProductsSolution() {ProductId=3,UserId=5,PurchaseDate=DateTime.Now,Price=400 },
                new WholesaleProductsSolution() {ProductId=4,UserId=2,PurchaseDate=DateTime.Now,Price=300 },
                new WholesaleProductsSolution() {ProductId=1,UserId=4,PurchaseDate=DateTime.Now,Price=800 },
                new WholesaleProductsSolution() {ProductId=1,UserId=5,PurchaseDate=DateTime.Now,Price=900 },
                new WholesaleProductsSolution() {ProductId=5,UserId=2,PurchaseDate=DateTime.Now,Price=1000 }
            };
            MockUsers = new List<Users>() {
                new Users(){UserId=1,UserName="Neha"},
                new Users(){UserId=2,UserName="Sumit"},
                new Users(){UserId=3,UserName="Rajan"},
                new Users(){UserId=4,UserName="Amit"},
                new Users(){UserId=5,UserName="Gayatri"}
            };
            MockProducts = new List<Product>() {
                new Product(){ProductId=1,ProductName="Mouse",ProductDescription="peripheral devices"},
                new Product(){ProductId=2,ProductName="Pendrive"},
                new Product(){ProductId=3,ProductName="Data Cabel",ProductDescription="requires cabel"},
                new Product(){ProductId=4,ProductName="Headphone",ProductDescription="requires cabel"},
                new Product(){ProductId=5,ProductName="KeyBoard",ProductDescription="peripheral devices"}
            };

        }

        //1.	Based on the trend of 7 days price for particular product id, recommend optimal purchasing price
        public double GetOptimalPrice(int productId)
        {
            var optimalPrice = MockData.Where(x => x.ProductId == productId && x.PurchaseDate >= DateTime.Now.AddDays(-7))
                .Average(y => y.Price);
            return optimalPrice;
        }

        //2.	Top 10 purchased products over a selected period
        public List<String> GetTop10PurchasedProducts(DateTime fromDate, DateTime toDate)
        {
            Product products = new Product();
            var optimalPrice = MockData.Where(x => x.PurchaseDate >= fromDate && x.PurchaseDate <= toDate)
                .Select(y=>y.ProductId)
                .Take(10);
            var productNames = from product in MockProducts
                               join
                               purchasedItem in optimalPrice
                               on product.ProductId equals purchasedItem
                               select product.ProductName;
            return productNames.ToList();
        } 
        
        //3.	Top 10 users based on total purchase value over a selected period
        public List<String> GetTop10UsersOnPurchaseValue(DateTime fromDate, DateTime toDate)
        {
            Users users = new Users();
            var topUser = from SelledProduct in MockData
                              //orderby SelledProduct.Price
                          where SelledProduct.PurchaseDate >= fromDate && SelledProduct.PurchaseDate <= toDate
                          group SelledProduct by SelledProduct.UserId into g
                          select new { UserId = g.Key, totalPrice = g.Sum(y => y.Price) };

            var usersList = (from user in topUser
                            join allUser in MockUsers
                            on user.UserId equals allUser.UserId
                            orderby user.totalPrice descending
                            select allUser.UserName)
                            .Take(10);
              
            return usersList.ToList();
        }

        //4.	Based on description of the product, predict related products
        public List<Product> GetRelatedProducts(int productId)
        {
            Product products = new Product();
            var relatedProductsList = (from pr in MockProducts
                                       where pr.ProductId == productId
                                       group pr by pr.ProductDescription into g
                                       select new Product {}
                                       ).ToList();
             
            return relatedProductsList;
        }
    }
}
