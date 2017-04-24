using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoParts
{
    class Database
    {
        private SqlConnection conn;

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
                    int custSupplierId = (int)reader["CustSupplierId"];
                    int quantityPerUnit = (int)reader["QuantityPerUnit"];
                    decimal costPrice = (decimal)reader["CostPrice"];
                    int unitsOnStock = (int)reader["UnitsOnStock"];
                    int unitsOnOrder = (int)reader["UnitsOnOrder"];
                    Boolean discontinued = (Boolean)reader["Discontinued"];
                    Products p = new Products(productId, productName, custSupplierId, quantityPerUnit, costPrice, unitsOnStock, unitsOnOrder, discontinued);
                    result.Add(p);
                }
            }
            return result;
        }

        public CustSuppliers FindSupplierById(int custSupplierId)
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM CustSuppliers WHERE CustSupplierId=" + custSupplierId, conn))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    string companyName = (string)reader["CompanyName"];
                    string contactName = (string)reader["ContactName"];
                    string contactTitle = (string)reader["ContactTitle"];
                    string address = (string)reader["Address"];
                    string phone = (string)reader["Phone"];
                    Boolean isCustomer = (Boolean)reader["IsCustomer"];
                    Boolean isSupplier = (Boolean)reader["IsSupplier"];

                    return new CustSuppliers(custSupplierId, companyName, contactName, contactTitle, address, phone, isCustomer, isSupplier);
                }
            }
            return null;
        }


        public Products FindProductById(int ProductId)
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM Products WHERE ProductId=" + ProductId, conn))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    string productName = (string)reader["ProductName"];
                    int custSupplierId = (int)reader["CustSupplierId"];
                    int quantityPerUnit = (int)reader["QuantityPerUnit"];
                    decimal costPrice = (decimal)reader["CostPrice"];
                    int unitsOnStock = (int)reader["UnitsOnStock"];
                    int unitsOnOrder = (int)reader["UnitsOnOrder"];
                    Boolean discontinued = (Boolean)reader["Discontinued"];

                    return new Products(ProductId, productName, custSupplierId, quantityPerUnit, costPrice, unitsOnStock, unitsOnOrder, discontinued);
                }
            }
            return null;
        }


        public List<Orders> AddPurchaseOrders(Orders o)
        {
            List<Orders> result = new List<Orders>();
            string sql = "INSERT INTO Orders (ProductId, CustSupplierId, EmployeeID, UnitPrice, Quantity, Discount, OrderDate, ShippedDate, ShippedAddress) "
                        + " VALUES (@ProductId, @CustSupplierId, @EmployeeID, @UnitPrice, @Quantity, @Discount, @OrderDate, @ShippedDate, @ShippedAddress)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = o.ProductId;
            cmd.Parameters.Add("@CustSupplierId", SqlDbType.Int).Value = o.CustSupplierID;
            cmd.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = o.EmployeeID;
            cmd.Parameters.Add("@UnitPrice", SqlDbType.Decimal).Value = o.UnitPrice;
            cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = o.Quantity;
            cmd.Parameters.Add("@Discount", SqlDbType.Decimal).Value = o.Discount;
            cmd.Parameters.Add("@OrderDate", SqlDbType.Date).Value = o.OrderDate;
            cmd.Parameters.Add("@ShippedDate", SqlDbType.Date).Value = o.ShippedDate;
            cmd.Parameters.Add("@ShippedAddress", SqlDbType.NVarChar).Value = o.ShippedAddress;
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            result.Add(o);
            return result;
        }
    }
}


