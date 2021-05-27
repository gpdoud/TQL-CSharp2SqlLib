using Microsoft.Data.SqlClient;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml.Linq;

namespace CSharp2SqlLib {
    
    public class ProductsController {

        private static Connection connection { get; set; }
        /// <summary>
        /// Creates a new product and selects the vendor by code
        /// </summary>
        /// <param name="product">A Product instance</param>
        /// <param name="VendorCode">A vendor code</param>
        /// <returns>True if successful otherwise false</returns>
        public bool Create(Product product, string VendorCode) {
            var vendCtrl = new VendorsController(connection);
            var vendor = vendCtrl.GetByCode(VendorCode);
            product.VendorId = vendor.Id;
            return Create(product);
        }

        private Product FillProductFromSqlRow(SqlDataReader reader) {
            var product = new Product() {
                Id = Convert.ToInt32(reader["Id"]),
                PartNbr = Convert.ToString(reader["PartNbr"]),
                Name = Convert.ToString(reader["Name"]),
                Price = Convert.ToDecimal(reader["Price"]),
                Unit = Convert.ToString(reader["Unit"]),
                PhotoPath = Convert.ToString(reader["PhotoPath"]),
                VendorId = Convert.ToInt32(reader["VendorId"])
            };
            return product;
        }

        public List<Product> GetAll() {
            var sql = "SELECT * from Products;";
            var cmd = new SqlCommand(sql, connection.SqlConn);
            var reader = cmd.ExecuteReader();
            var products = new List<Product>();
            while(reader.Read()) {
                var product = FillProductFromSqlRow(reader);  
                products.Add(product);
            }
            reader.Close();
            //GetVendorForProducts(products);
            return products;
        }
        public Product GetByPK(int id) {
            var sql = "SELECT * from Products Where Id = @id;";
            var cmd = new SqlCommand(sql, connection.SqlConn);
            cmd.Parameters.AddWithValue("@id", id);
            var reader = cmd.ExecuteReader();
            reader.Read();
            var product = FillProductFromSqlRow(reader);
            reader.Close();
            GetVendorForProduct(product);
            return product;
        }
        public bool Create(Product product) {
            var sql = "INSERT into Products "
                        + " (PartNbr, Name, Price, Unit, PhotoPath, VendorId) "
                        + " VALUES (@partnbr, @name, @price, @unit, @photopath, @vendorid); ";
            var cmd = new SqlCommand(sql, connection.SqlConn);
            //cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@partnbr", product.PartNbr);
            cmd.Parameters.AddWithValue("@name", product.Name);
            cmd.Parameters.AddWithValue("@price", product.Price);
            cmd.Parameters.AddWithValue("@unit", product.Unit);
            cmd.Parameters.AddWithValue("@photopath", (object)product.PhotoPath ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@vendorid", product.VendorId);
            var rowsAffected = cmd.ExecuteNonQuery();
            return (rowsAffected == 1);
        }
        private void GetVendorForProducts(List<Product> products) {
            foreach(var product in products) {
                GetVendorForProduct(product);
            }
        }
        private void GetVendorForProduct(Product product) {
            var vendCtrl = new VendorsController(connection);
            product.Vendor = vendCtrl.GetByPK(product.VendorId);
        }
        public ProductsController(Connection connection) {
            ProductsController.connection = connection;
        }
    }
}
