using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApp
{
    //PartTimeEmployee is inheriting from Employee
    public class PartTimeEmployee : Employee
    {
        #region Properties

        public decimal HourlyRate { get; set; }

        public double HoursWorked { get; set; }

        #endregion

        #region Constructors

        public PartTimeEmployee(string firstName, string lastName, decimal hourlyRate, double hoursWorked) : base(firstName, lastName)
        {
            HourlyRate = hourlyRate;
            HoursWorked = hoursWorked;
        }

        public PartTimeEmployee(string firstName, string lastName) : base(firstName, lastName) { }

        public PartTimeEmployee() : base("Unknown", "Unknown") { }

        #endregion

        #region Methods

        public override string ToString()//Setting new ToString Method
        {
            return string.Format("{0}, {1} - Part Time", LastName.ToUpper(), FirstName);
        }

        public override decimal CalculateMonthlyPay()//Overriding and making use of abstract method
        {
            decimal pay = (decimal)HoursWorked * HourlyRate;
            return pay;
        }

        #endregion
    }
}
