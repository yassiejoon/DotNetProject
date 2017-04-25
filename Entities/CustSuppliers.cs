using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoParts
{
    class CustSuppliers
    {
        public int CustSupplierId;
        public string CompanyName;
        public string ContactName;
        public string ContactTitle;
        public string Address;
        public string Phone;
        public Boolean IsCustomer;
        public Boolean IsSupplier;

        public CustSuppliers(int custSupplierId, string companyName, string contactName,
        string contactTitle, string address, string phone, Boolean isCustomer, Boolean isSupplier)
        {
            this.CustSupplierId = custSupplierId;
            this.CompanyName = companyName;
            this.ContactName = contactName;
            this.ContactTitle = contactTitle;
            this.Address = address;
            this.Phone = phone;
            this.IsCustomer = isCustomer;
            this.IsSupplier = isSupplier;
        }
    }
}

