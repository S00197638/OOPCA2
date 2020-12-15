namespace EmployeeApp
{
    //FullTimeEmployee is inheriting from Employee
    public class FullTimeEmployee : Employee
    {
        #region Properties

        public decimal Salary { get; set; }

        #endregion

        #region Constructors

        public FullTimeEmployee(string firstName, string lastName, decimal salary) : base(firstName, lastName)
        {
            Salary = salary;
        }

        public FullTimeEmployee(string firstName, string lastName) : base(firstName, lastName) { }

        public FullTimeEmployee() : base("Unknown", "Unknown") { }

        #endregion

        #region Methods

        public override string ToString()//Setting new ToString Method
        {
            return string.Format("{0}, {1} - Full Time", LastName.ToUpper(), FirstName);
        }

        public override decimal CalculateMonthlyPay()//Overriding and making use of abstract method
        {
            return Salary / 12;
        }

        #endregion
    }
}
