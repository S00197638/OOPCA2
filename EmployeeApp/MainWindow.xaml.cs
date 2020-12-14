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

        private void CkBxIsChecked()
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
                    if(selectedEmployee is FullTimeEmployee)
                    {
                        //if (ckbxPartTime.IsChecked == true)
                        //{
                        //    Employee emp = selectedEmployee;
                        //    PartTimeEmployee newPTTemp = (PartTimeEmployee)emp;
                        //    UpdatePTEmployee(newPTTemp, selectedEmployee);
                        //    //Display error message
                        //    if (tbxHoursWorked.Text == "Hours Worked..." && tbxHoursWorked.Text == "" && 
                        //        tbxHourlyRate.Text == "Hourly Rate..." && tbxHourlyRate.Text == "")
                        //    {
                        //        MessageBox.Show("To Update an Employee, You Must Enter All Relevant Data!");
                        //        tbxHoursWorked.Text = "Hours Worked...";
                        //        tbxHourlyRate.Text = "Hourly Rate...";
                        //        tblkMonthlyPay.Text = "";
                        //    }
                        //}
                        //else if(ckbxFullTime.IsChecked == true)
                        //{
                        //    FullTimeEmployee temp = (FullTimeEmployee)selectedEmployee;
                        //    UpdateFTEmployee(temp, selectedEmployee);
                        //    if(tbxSalary.Text == "Salary..." && tbxSalary.Text == "")//Display error message
                        //    {
                        //        MessageBox.Show("To Update an Employee, You Must Enter All Relevant Data!");
                        //        tbxSalary.Text = temp.Salary.ToString();
                        //        tblkMonthlyPay.Text = temp.CalculateMonthlyPay().ToString("€0.00");
                        //    }
                        //}
                    }
                    else
                    {
                        //Updating employee with inputted data
                        PartTimeEmployee temp = (PartTimeEmployee)selectedEmployee;
                        if(tbxHoursWorked.Text != "Hours Worked..." && tbxHoursWorked.Text != "" &&
                            tbxHourlyRate.Text != "Hourly Rate..." && tbxHourlyRate.Text != "")
                        {
                            temp.FirstName = tbxFirstName.Text;
                            temp.LastName = tbxLastName.Text;
                            temp.HoursWorked = Convert.ToDouble(tbxHoursWorked.Text);
                            temp.HourlyRate = Convert.ToDecimal(tbxHourlyRate.Text);
                            employees.Remove(selectedEmployee);//Remove the old employee object from the list
                            employees.Add(temp);//Add the new updated version of the employee to the list
                            lbxEmployees.ItemsSource = null;//Refreshing the display in the listbox
                            CkBxIsChecked();//Check Filtering
                            lbxEmployees.SelectedItem = temp;//Setting the selected item to the new updated employee
                        }
                        else//Display error message
                        {
                            MessageBox.Show("To Update an Employee, You Must Enter All Relevant Data!");
                            tbxHoursWorked.Text = temp.HoursWorked.ToString();
                            tbxHourlyRate.Text = temp.HourlyRate.ToString();
                            tblkMonthlyPay.Text = temp.CalculateMonthlyPay().ToString("€0.00");
                        }
                    }
                }
                else//Show error message
                {
                    MessageBox.Show("To Update an Employee, You Must Enter:\nFirst Name,\nLast Name,\nIf FT or PT," +
                        "\n& All Relevant Data!");
                    //Reset back to old data
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
            }
            else//Show Error message
                MessageBox.Show("To Update an Employee, You Must Select an Employee from the Employees' List!");
        }

        //private void UpdatePTEmployee(PartTimeEmployee temp, Employee selectedEmployee)
        //{
        //    //Updating employee with inputted data
        //    if (tbxHoursWorked.Text != "Hours Worked..." && tbxHoursWorked.Text != "" && 
        //        tbxHourlyRate.Text != "Hourly Rate..." && tbxHourlyRate.Text != "")
        //    {
        //        temp.FirstName = tbxFirstName.Text;
        //        temp.LastName = tbxLastName.Text;
        //        temp.HoursWorked = Convert.ToDouble(tbxHoursWorked.Text);
        //        temp.HourlyRate = Convert.ToDecimal(tbxHourlyRate.Text);
        //        employees.Remove(selectedEmployee);//Remove the old employee object from the list
        //        employees.Add(temp);//Add the new updated version of the employee to the list
        //        lbxEmployees.ItemsSource = null;//Refreshing the display in the listbox
        //        CkBxIsChecked();//Check Filtering
        //        lbxEmployees.SelectedItem = temp;//Setting the selected item to the new updated employee
        //    }
        //}

        //private void UpdateFTEmployee(FullTimeEmployee temp, Employee selectedEmployee)
        //{
        //    //Updating employee with inputted data
        //    if (tbxSalary.Text != "Salary..." && tbxSalary.Text != "")
        //    {
        //        temp.FirstName = tbxFirstName.Text;
        //        temp.LastName = tbxLastName.Text;
        //        temp.Salary = Convert.ToDecimal(tbxSalary.Text);
        //        employees.Remove(selectedEmployee);//Remove the old employee object from the list
        //        employees.Add(temp);//Add the new updated version of the employee to the list
        //        lbxEmployees.ItemsSource = null;//Refreshing the display in the listbox
        //        CkBxIsChecked();//Check Filtering
        //        lbxEmployees.SelectedItem = temp;//Setting the selected item to the new updated employee
        //    }
        //}

        //private void UpdateFTEmployee(FullTimeEmployee temp, Employee selectedEmployee)
        //{
        //    //Updating employee with inputted data
        //    if (tbxSalary.Text != "Salary..." && tbxSalary.Text != "")
        //    {
        //        temp.FirstName = tbxFirstName.Text;
        //        temp.LastName = tbxLastName.Text;
        //        temp.Salary = Convert.ToDecimal(tbxSalary.Text);
        //        employees.Remove(selectedEmployee);//Remove the old employee object from the list
        //        employees.Add(temp);//Add the new updated version of the employee to the list
        //        lbxEmployees.ItemsSource = null;//Refreshing the display in the listbox
        //        CkBxIsChecked();//Check Filtering
        //        lbxEmployees.SelectedItem = temp;//Setting the selected item to the new updated employee



        //        if (ckbxPartTime.IsChecked == true)
        //        {
        //            Employee emp = selectedEmployee;
        //            FullTimeEmployee newFTTemp = (FullTimeEmployee)emp;
        //            UpdateFTEmployee(newFTTemp, selectedEmployee);
        //            if (tbxSalary.Text == "Salary..." && tbxSalary.Text == "")//Display error message
        //            {
        //                MessageBox.Show("To Update an Employee, You Must Enter All Relevant Data!");
        //                tbxSalary.Text = "Salary...";
        //                tblkMonthlyPay.Text = "";
        //            }
        //        }
        //        else
        //        {
        //            FullTimeEmployee temp = (FullTimeEmployee)selectedEmployee;
        //            UpdateFTEmployee(temp, selectedEmployee);
        //            if (tbxSalary.Text == "Salary..." && tbxSalary.Text == "")//Display error message
        //            {
        //                MessageBox.Show("To Update an Employee, You Must Enter All Relevant Data!");
        //                tbxSalary.Text = temp.Salary.ToString();
        //                tblkMonthlyPay.Text = temp.CalculateMonthlyPay().ToString("€0.00");
        //            }
        //        }
        //    }
        //}
    }
}
