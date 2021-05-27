using CSharp2SqlLib;

using System;

namespace TestCSharp2SQlLib {
    class Program {
        static void Main(string[] args) {

            var sqlconn = new Connection("localhost\\sqlexpress", "PrsDb");
            var newProduct = new Product() {
                Id = 0, PartNbr = "SKYLINE", Name = "Skyline Chili", Price = 5,
                Unit = "Each", PhotoPath = null, VendorId = 0
            };
            var productsController = new ProductsController(sqlconn);
            var success = productsController.Create(newProduct, "KROG");

            var vendorsController = new VendorsController(sqlconn);
            var vendors = vendorsController.GetAll();

            var products = productsController.GetAll();
            var product = productsController.GetByPK(1);
            Console.WriteLine(product);

            sqlconn.Disconnect();

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
