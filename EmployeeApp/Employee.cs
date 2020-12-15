using System;

namespace EmployeeApp
{
    //Implementing IComparable Interface for sorting
    public abstract class Employee : IComparable
    {
        #region Properties

        public string FirstName { get; set; }

        public string LastName { get; set; }

        #endregion

        #region Constructors

        public Employee(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        #endregion

        #region Methods

        public abstract decimal CalculateMonthlyPay();//Abstract method, that child classes can override

        public int CompareTo(object otherObject)//To list the employees alphabetically
        {
            Employee temp = (Employee)otherObject;
            int returnValue = this.LastName.CompareTo(temp.LastName);//Sort by LastName

            if (returnValue == 0)
                returnValue = this.FirstName.CompareTo(temp.FirstName);//If LastName is the same, Sort by FirstName

            return returnValue;
        }

        #endregion
    }
}
