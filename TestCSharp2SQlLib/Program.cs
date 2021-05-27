using CSharp2SqlLib;

using System;
using System.Linq;

namespace TestCSharp2SQlLib {
    class Program {

        static int[] ints = {
            505,916,549,881,918,385,350,228,489,719,
            866,252,130,706,581,313,767,691,678,187,
            115,660,653,564,805,720,729,392,598,791,
            620,345,292,318,726,501,236,573,890,357,
            854,212,670,782,267,455,579,849,229,661,
            611,588,703,607,824,730,239,118,684,149,
            206,952,531,809,134,929,593,385,520,214,
            643,191,998,555,656,738,829,454,195,419,
            326,996,666,242,189,464,553,579,188,884,
            197,369,435,476,181,192,439,615,746,277
        };

        static void Main(string[] args) {

            var sum711 = ints.Where(i => i % 7 == 0 || i % 11 == 0).Sum();

            sum711 = (from i in ints
                      where i % 7 == 0 || i % 11 == 0
                      select i).Sum();

            var avg = ints.Where(x => x % 3 == 0 || x % 5 == 0).Average();

            var avg0 = (from i in ints
                       where i % 3 == 0 || i % 5 == 0 
                       select i).Average();

            var sum = 0;
            var cnt = 0;
            foreach(var i in ints) {
                if(i % 3 == 0) {
                    sum += i;
                    cnt++;
                }
            }
            var avg1 = sum / cnt; 


            {

                var sqlconn = new Connection("localhost\\sqlexpress", "PrsDb");
                //var newProduct = new Product() {
                //    Id = 0, PartNbr = "SKYLINE", Name = "Skyline Chili", Price = 5,
                //    Unit = "Each", PhotoPath = null, VendorId = 0
                //};
                var productsController = new ProductsController(sqlconn);
                ////var success = productsController.Create(newProduct, "KROG");

                //var vendorsController = new VendorsController(sqlconn);
                //var vendors = vendorsController.GetAll();

                var products = productsController.GetAll();
                //var product = productsController.GetByPK(1);
                //Console.WriteLine(product);

                sqlconn.Disconnect();

                var productPriceAvg = (from p in products
                                       select p).Average(p => p.Price);
            }
            #region Commented Out Code
            //var sqllib = new SqlLib();
            //sqllib.Connect();

            //var user = sqllib.GetByPK(7);
            //user.Phone = "513-555-1212";
            //var success = sqllib.Change(user);


            //var newUser = new User() {
            //    Id = 0, Username = "XYZ1", Password = "XYZ", Firstname = "XYZ", Lastname = "XYZ",
            //    Phone = "XYZ", Email = "XYZ", IsReviewer = true, IsAdmin = true
            //};
            ////var success = sqllib.Create(newUser);

            //var users = sqllib.GetAllUsers();
            //var nulluser = sqllib.GetByPK(0);

            //sqllib.Disconnect();
            #endregion
        }
    }
}
