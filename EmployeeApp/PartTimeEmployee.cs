using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApp
{
    public class PartTimeEmployee : Employee
    {
        public decimal HourlyRate { get; set; }

        public double HoursWorked { get; set; }

        public override decimal CalculateMonthlyPay()
        {
            decimal pay = (decimal)HoursWorked * HourlyRate;
            return pay;
        }

    }
}
