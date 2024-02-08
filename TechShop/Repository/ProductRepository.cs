﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Model;
using TechShop.Utility;

namespace TechShop.Repository
{
    internal class ProductRepository : IProductRepository
    {
        public string connectionString;
        SqlConnection sqlConnection = null;
        SqlCommand cmd = null;
        public ProductRepository()
        {
            connectionString = DBConnectionUtility.GetConnectedString();
            cmd = new SqlCommand();
        }

        public Product GetProductDetails(int id)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    cmd.CommandText = "select * from products where ProductID=@id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Connection = conn;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Product product = new Product();
                        product.ProductID = (int)reader["ProductID"];
                        product.ProductName = (string)reader["ProductName"];
                        product.Description = (string)reader["Description"];
                        product.Price = (decimal)reader["Price"];
                        return product;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public bool IsProductInStock(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString)) {
                conn.Open();
                cmd.CommandText = "select  ";

            }
        }

        public bool UpdateProductInfo(int id)
        {
            bool result = false;
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    cmd.CommandText = "update Products set price = price+(price*0.1) where ProductID=@id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Connection = conn;
                    int rowsEffected = cmd.ExecuteNonQuery();
                    result = rowsEffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
    }
}
