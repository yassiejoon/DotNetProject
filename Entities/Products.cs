using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoParts
{
    class Products
    {
        public int ProductId;
        public string ProdName;
        public int SupplierId;
        public int QuantityPerUnit;
        public decimal CostPrice;
        public int UnitsOnStock;
        public int UnitsOnOrder;
        Boolean Discontinued;

        public Products(int productId, string prodName, int supplierId, int quantityPerUnit, 
            decimal costPrice, int unitsOnStock, int unitsOnOrder, Boolean discontinued)
        {
            this.ProductId = productId;
            this.ProdName = prodName;
            this.SupplierId = supplierId;
            this.QuantityPerUnit = quantityPerUnit;
            this.CostPrice = costPrice;
            this.UnitsOnStock = unitsOnStock;
            this.UnitsOnOrder = unitsOnOrder;
            this.Discontinued = discontinued;
        }
    }
}
