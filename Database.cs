using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoParts
{
    class Database
    {
        SqlConnection conn;

        public Database()
        {
            conn = new SqlConnection(@"Data Source=wei-abbott.database.windows.net;Initial Catalog=myprject_YW;Persist Security Info=True;User ID=dbadmin;Password=JohnIsGreat2000");
            conn.Open();
        }

        public List<Products> GetAllProducts()
        {
            List<Products> result = new List<Products>();
            using (SqlCommand command = new SqlCommand("SELECT * FROM Products", conn))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int productId = (int)reader["ProductId"];
                    
                    string productName = (string)reader["ProductName"];
                    string custSupplierId = (string)reader["CustSupplierId"];
                    decimal quantityPerUnit = (decimal)reader["QuantityPerUnit"];
                    decimal costPrice = (decimal)reader["CostPrice"];
                    int unitsOnStock = (int)reader["UnitsOnStock"];
                    int unitsOnOrder = (int)reader["UnitsOnOrder"];
                    Boolean discontinued = (Boolean)reader["Discontinued"];
                }
            }
            return result;
        }
    }
}
