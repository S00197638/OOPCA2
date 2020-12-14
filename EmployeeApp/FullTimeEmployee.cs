using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApp
{
    public class FullTimeEmployee : Employee
    {
        public decimal Salary { get; set; }

        public FullTimeEmployee(string firstName, string lastName, decimal salary) : base(firstName, lastName)
        {
            Salary = salary;
        }

        public FullTimeEmployee(string firstName, string lastName) : base(firstName, lastName) { }

        public FullTimeEmployee() : base("Unknown", "Unknown") { }

        public override string ToString()
        {
            return string.Format("{0}, {1} - Full Time", LastName.ToUpper(), FirstName);
        }

        public override decimal CalculateMonthlyPay()
        {
            return Salary / 12;
        }

    }
}
