using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AutoParts
{
    class Employees
    {
        public int EmployeeId;
        public string FirstName;
        public string LastName;
        public string UserName;
        public string Password;
        public Boolean IsAdmin;

        public Employees( int employeeId, string firstName, string lastName, string userName, string password, Boolean isAdmin)
        {
            this.EmployeeId = employeeId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.UserName = userName;
            this.Password = password;
            this.IsAdmin = isAdmin;
        }
    }
}
