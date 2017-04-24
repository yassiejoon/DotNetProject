using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoParts
{
    class Orders
    {
        public int OrderId;
        public int ProductId;
        public int CustomerID;
        public int EmployeeID;
        public decimal UnitPrice;
        public int Quantity;
        public decimal Discount;
        public DateTime OrderDate;
        public DateTime ShippedDate;
        public string ShippedAddress;

        public Orders(int orderId, int productId, int customerID, int employeeID, decimal unitPrice,
            int quantity, decimal discount, DateTime orderDate, DateTime shippedDate, string shippedAddress)
        {
            this.OrderId = orderId;
            this.ProductId = productId;
            this.CustomerID = customerID;
            this.EmployeeID = employeeID;
            this.UnitPrice = unitPrice;
            this.Quantity = quantity;
            this.Discount = discount;
            this.OrderDate = orderDate;
            this.ShippedDate = shippedDate;
            this.ShippedAddress = shippedAddress;
        }
    }
}
