using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApp
{
    public abstract class Employee : IComparable
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Employee(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public abstract decimal CalculateMonthlyPay();

        public int CompareTo(object otherObject)
        {
            Employee temp = (Employee)otherObject;
            int returnValue = this.LastName.CompareTo(temp.LastName);//Sort by LastName

            if (returnValue == 0)
                returnValue = this.FirstName.CompareTo(temp.FirstName);//If LastName is the same, Sort by FirstName

            return returnValue;
        }
    }
}
