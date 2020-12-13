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

        public PartTimeEmployee(string firstName, string lastName, decimal hourlyRate, double hoursWorked) : base(firstName, lastName)
        {
            HourlyRate = hourlyRate;
            HoursWorked = hoursWorked;
        }

        public PartTimeEmployee(string firstName, string lastName) : base(firstName, lastName) { }

        public PartTimeEmployee() : base("Unknown", "Unknown") { }

        public override string ToString()
        {
            return string.Format("{0}, {1} - Part Time", LastName.ToUpper(), FirstName);
        }

        public override decimal CalculateMonthlyPay()
        {
            decimal pay = (decimal)HoursWorked * HourlyRate;
            return pay;
        }

    }
}
