using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EmployeeApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Employee> employees = new List<Employee>();
        List<Employee> filteredEmployees = new List<Employee>();

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Setup

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Create Initial Employees
            CreateInitialEmployees();

            //Set CheckBoxes
            ckbxFullTime.IsChecked = true;
            ckbxPartTime.IsChecked = true;

            //Check what checkboxes are checked
            CkBxIsChecked();
        }

        private void CreateInitialEmployees()
        {
            //Create New Employees
            FullTimeEmployee fEmp1 = new FullTimeEmployee("Leopold", "Fitz", 32000);
            FullTimeEmployee fEmp2 = new FullTimeEmployee("Gemma", "Simmons", 35000);
            PartTimeEmployee pEmp1 = new PartTimeEmployee("Grant", "Ward", 8.5m, 10);
            PartTimeEmployee pEmp2 = new PartTimeEmployee("Daisy", "Johnson", 9.5m, 10);

            //Add Employees To List
            employees.Add(fEmp1);
            employees.Add(fEmp2);
            employees.Add(pEmp1);
            employees.Add(pEmp2);
        }

        #endregion

        #region Filtering

        private void CkBxIsChecked()//Filtering Check
        {
            if (ckbxFullTime.IsChecked == true && ckbxPartTime.IsChecked == true)//List all Employees in alphabetical order
            {
                employees.Sort();
                lbxEmployees.ItemsSource = employees;
            }
            else if (ckbxFullTime.IsChecked == true && ckbxPartTime.IsChecked == false)//List all FullTime Employees in alphabetical order
            {
                filteredEmployees.Clear();

                foreach (Employee employee in employees)
                {
                    if (employee is FullTimeEmployee)
                        filteredEmployees.Add(employee);
                }

                filteredEmployees.Sort();
                lbxEmployees.ItemsSource = filteredEmployees;
            }
            else if (ckbxPartTime.IsChecked == true && ckbxFullTime.IsChecked == false)//List all PartTime Employees in alphabetical order
            {
                filteredEmployees.Clear();
                
                foreach (Employee employee in employees)
                {
                    if (employee is PartTimeEmployee)
                        filteredEmployees.Add(employee);
                }

                filteredEmployees.Sort();
                lbxEmployees.ItemsSource = filteredEmployees;
            }
            else if(ckbxFullTime.IsChecked == false && ckbxPartTime.IsChecked == false)//Show no Employees
            {
                lbxEmployees.ItemsSource = null;
            }
        }

        private void ckbxFullTime_Checked(object sender, RoutedEventArgs e)//Checking for change in state of the checkbox
        {
            CkBxIsChecked();
        }

        private void ckbxFullTime_Unchecked(object sender, RoutedEventArgs e)//Checking for change in state of the checkbox
        {
            CkBxIsChecked();
        }

        private void ckbxPartTime_Checked(object sender, RoutedEventArgs e)//Checking for change in state of the checkbox
        {
            CkBxIsChecked();
        }

        private void ckbxPartTime_Unchecked(object sender, RoutedEventArgs e)//Checking for change in state of the checkbox
        {
            CkBxIsChecked();
        }

        #endregion

        #region Selection Display

        private void lbxEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)//Change of selection in listbox
        {
            Employee selectedEmployee = lbxEmployees.SelectedItem as Employee;//Saving the Data of selected Employee in another employee object

            if(selectedEmployee != null)//Making sure an employee is selected
            {
                //Setting the fields on the right of the display to the selected employee's data
                tbxFirstName.Text = selectedEmployee.FirstName;
                tbxLastName.Text = selectedEmployee.LastName;
                if (selectedEmployee is FullTimeEmployee)
                {
                    FullTimeEmployee temp = (FullTimeEmployee)selectedEmployee;
                    rbtnFullTime.IsChecked = true;
                    tbxSalary.Text = temp.Salary.ToString();
                    tbxHoursWorked.Text = "";
                    tbxHourlyRate.Text = "";
                    tblkMonthlyPay.Text = temp.CalculateMonthlyPay().ToString("€0.00");
                }
                else
                {
                    PartTimeEmployee temp = (PartTimeEmployee)selectedEmployee;
                    rbtnPartTime.IsChecked = true;
                    tbxSalary.Text = "";
                    tbxHoursWorked.Text = temp.HoursWorked.ToString();
                    tbxHourlyRate.Text = temp.HourlyRate.ToString();
                    tblkMonthlyPay.Text = temp.CalculateMonthlyPay().ToString("€0.00");
                }
            }
        }

        #endregion

        #region Add Button

        private void btnAdd_Click(object sender, RoutedEventArgs e)//When Add Button is clicked
        {
            //Checking if there is valid data inputted
            if (tbxFirstName.Text != "First Name..." && tbxFirstName.Text != "" &&
                tbxLastName.Text != "Last Name..." && tbxLastName.Text != "" &&
                (rbtnFullTime.IsChecked != false || rbtnPartTime.IsChecked != false))
                AddNewEmployee();
            else//Show Error Message
            {
                MessageBox.Show("To Add an Employee, You Must Enter:\nFirst Name,\nLast Name,\n& If FT or PT!");
                tbxFirstName.Text = "First Name...";
                tbxLastName.Text = "Last Name...";
                rbtnFullTime.IsChecked = false;
                rbtnPartTime.IsChecked = false;
                tbxSalary.Text = "Salary...";
                tbxHoursWorked.Text = "Hours Worked...";
                tbxHourlyRate.Text = "Hourly Rate...";
                tbxFirstName.Background = Brushes.Red;
                tbxLastName.Background = Brushes.Red;
                rbtnFullTime.Background = Brushes.Red;
                rbtnPartTime.Background = Brushes.Red;
            }
        }

        private void AddNewEmployee()
        {
            FullTimeEmployee newFTEmp;
            PartTimeEmployee newPTEmp;

            //Setting inputted data, the data for new employee
            string firstName = tbxFirstName.Text;
            string lastName = tbxLastName.Text;
            if (rbtnFullTime.IsChecked == true)
            {
                if (tbxSalary.Text != "Salary..." && tbxSalary.Text != "")
                {
                    decimal salary = Convert.ToDecimal(tbxSalary.Text);
                    newFTEmp = new FullTimeEmployee(firstName, lastName, salary);//Creating new employee object
                }
                else
                    newFTEmp = new FullTimeEmployee(firstName, lastName);//Creating new employee object

                employees.Add(newFTEmp);//Adding the new employee to the list of employees
                lbxEmployees.ItemsSource = null;//Refreshing the display in the listbox
                CkBxIsChecked();//Check Filtering
            }
            else if (rbtnPartTime.IsChecked == true)
            {
                if (tbxHoursWorked.Text != "Hours Worked..." && tbxHoursWorked.Text != "" &&
                    tbxHourlyRate.Text != "Hourly Rate..." && tbxHourlyRate.Text != "")
                {
                    double hoursWorked = Convert.ToDouble(tbxHoursWorked.Text);
                    decimal hourlyRate = Convert.ToDecimal(tbxHourlyRate.Text);
                    newPTEmp = new PartTimeEmployee(firstName, lastName, hourlyRate, hoursWorked);//Creating new employee object
                }
                else
                    newPTEmp = new PartTimeEmployee(firstName, lastName);//Creating new employee object

                employees.Add(newPTEmp);//Adding the new employee to the list of employees
                lbxEmployees.ItemsSource = null;//Refreshing the display in the listbox
                CkBxIsChecked();//Check Filtering
            }
        }

        #endregion

        #region Interactions

        private void tbxFirstName_GotFocus(object sender, RoutedEventArgs e)//Clearing Field
        {
            tbxFirstName.Clear();
            tbxFirstName.Background = Brushes.White;
        }

        private void tbxLastName_GotFocus(object sender, RoutedEventArgs e)//Clearing Field
        {
            tbxLastName.Clear();
            tbxLastName.Background = Brushes.White;
        }

        private void tbxSalary_GotFocus(object sender, RoutedEventArgs e)//Clearing Field
        {
            tbxSalary.Clear();
        }

        private void tbxHoursWorked_GotFocus(object sender, RoutedEventArgs e)//Clearing Field
        {
            tbxHoursWorked.Clear();
        }

        private void tbxHourlyRate_GotFocus(object sender, RoutedEventArgs e)//Clearing Field
        {
            tbxHourlyRate.Clear();
        }

        private void rbtnFullTime_Click(object sender, RoutedEventArgs e)//When radio button clicked
        {
            tbxHoursWorked.Clear();
            tbxHourlyRate.Clear();
            tbxSalary.Text = "Salary...";
            rbtnFullTime.Background = Brushes.White;
            rbtnPartTime.Background = Brushes.White;
        }

        private void rbtnPartTime_Click(object sender, RoutedEventArgs e)//When radio button clicked
        {
            tbxSalary.Clear();
            tbxHoursWorked.Text = "Hours Worked...";
            tbxHourlyRate.Text = "Hourly Rate...";
            rbtnFullTime.Background = Brushes.White;
            rbtnPartTime.Background = Brushes.White;
        }

        #endregion

        #region Update Button

        private void btnUpdate_Click(object sender, RoutedEventArgs e)//When Update button is clicked
        {
            Employee selectedEmployee = lbxEmployees.SelectedItem as Employee;//Saving the Data of selected Employee in another employee object

            if (selectedEmployee != null)//Making sure an employee is selected
            {
                //Checking if there is valid data inputted
                if (tbxFirstName.Text != "First Name..." && tbxFirstName.Text != "" &&
                tbxLastName.Text != "Last Name..." && tbxLastName.Text != "" &&
                (rbtnFullTime.IsChecked != false || rbtnPartTime.IsChecked != false))
                {
                    FullTimeEmployee tempFT = null;
                    PartTimeEmployee tempPT = null;

                    if (selectedEmployee is FullTimeEmployee)
                        UpdateFTEmployee(tempFT, tempPT, selectedEmployee);//Updating FT Employee
                    else
                        UpdatePTEmployee(tempFT, tempPT, selectedEmployee);//Updating PT Employee
                }
                else//Show error message
                {
                    MessageBox.Show("To Update an Employee, You Must Enter:\nFirst Name,\nLast Name,\nIf FT or PT," +
                        "\n& All Relevant Data!");
                    ResetData(selectedEmployee);//Reset back to old data
                }
            }
            else//Show Error message
                MessageBox.Show("To Update an Employee, You Must Select an Employee from the Employees' List!");
        }

        private void UpdateFTEmployee(FullTimeEmployee tempFT, PartTimeEmployee tempPT, Employee selectedEmployee)//Updating FT Employee
        {
            //Updating employee with inputted data
            tempFT = (FullTimeEmployee)selectedEmployee;
            if (tbxSalary.Text != "Salary..." && tbxSalary.Text != "")
            {
                tempFT.FirstName = tbxFirstName.Text;
                tempFT.LastName = tbxLastName.Text;
                tempFT.Salary = Convert.ToDecimal(tbxSalary.Text);
                UpdateEmployees(tempFT, tempPT, selectedEmployee);
            }
            else//Display error message
            {
                MessageBox.Show("To Update an Employee, You Must Enter All Relevant Data!");
                tbxSalary.Text = tempFT.Salary.ToString();
                tblkMonthlyPay.Text = tempFT.CalculateMonthlyPay().ToString("€0.00");
            }
        }

        private void UpdatePTEmployee(FullTimeEmployee tempFT, PartTimeEmployee tempPT, Employee selectedEmployee)//Updating PT Employee
        {
            //Updating employee with inputted data
            tempPT = (PartTimeEmployee)selectedEmployee;
            if (tbxHoursWorked.Text != "Hours Worked..." && tbxHoursWorked.Text != "" &&
                tbxHourlyRate.Text != "Hourly Rate..." && tbxHourlyRate.Text != "")
            {
                tempPT.FirstName = tbxFirstName.Text;
                tempPT.LastName = tbxLastName.Text;
                tempPT.HoursWorked = Convert.ToDouble(tbxHoursWorked.Text);
                tempPT.HourlyRate = Convert.ToDecimal(tbxHourlyRate.Text);
                UpdateEmployees(tempFT, tempPT, selectedEmployee);
            }
            else//Display error message
            {
                MessageBox.Show("To Update an Employee, You Must Enter All Relevant Data!");
                tbxHoursWorked.Text = tempPT.HoursWorked.ToString();
                tbxHourlyRate.Text = tempPT.HourlyRate.ToString();
                tblkMonthlyPay.Text = tempPT.CalculateMonthlyPay().ToString("€0.00");
            }
        }

        private void UpdateEmployees(FullTimeEmployee tempFT, PartTimeEmployee tempPT, Employee selectedEmployee)//Updating Employees' List/Refreshing Data
        {
            if(tempFT != null)
            {
                employees.Remove(selectedEmployee);//Remove the old employee object from the list
                employees.Add(tempFT);//Add the new updated version of the employee to the list
                lbxEmployees.ItemsSource = null;//Refreshing the display in the listbox
                CkBxIsChecked();//Check Filtering
                lbxEmployees.SelectedItem = tempFT;//Setting the selected item to the new updated employee
            }
            else
            {
                employees.Remove(selectedEmployee);//Remove the old employee object from the list
                employees.Add(tempPT);//Add the new updated version of the employee to the list
                lbxEmployees.ItemsSource = null;//Refreshing the display in the listbox
                CkBxIsChecked();//Check Filtering
                lbxEmployees.SelectedItem = tempPT;//Setting the selected item to the new updated employee
            }
        }

        private void ResetData(Employee selectedEmployee)//Resetting back to old Data
        {
            tbxFirstName.Text = selectedEmployee.FirstName;
            tbxLastName.Text = selectedEmployee.LastName;
            if (selectedEmployee is FullTimeEmployee)
            {
                FullTimeEmployee temp = (FullTimeEmployee)selectedEmployee;
                rbtnFullTime.IsChecked = true;
                tbxSalary.Text = temp.Salary.ToString();
                tblkMonthlyPay.Text = temp.CalculateMonthlyPay().ToString("€0.00");
            }
            else
            {
                PartTimeEmployee temp = (PartTimeEmployee)selectedEmployee;
                rbtnPartTime.IsChecked = true;
                tbxHoursWorked.Text = temp.HoursWorked.ToString();
                tbxHourlyRate.Text = temp.HourlyRate.ToString();
                tblkMonthlyPay.Text = temp.CalculateMonthlyPay().ToString("€0.00");
            }
        }

        #endregion
    }
}