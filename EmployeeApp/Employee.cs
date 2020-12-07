using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApp
{
    public abstract class Employee
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual decimal CalculateMonthlyPay()
        {
            decimal monthlyPay = 0;
            return monthlyPay;
        }

    }
}
