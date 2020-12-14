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
            FullTimeEmployee fEmp1 = new FullTimeEmployee("Leopold", "Fitz", 32000);
            FullTimeEmployee fEmp2 = new FullTimeEmployee("Gemma", "Simmons", 35000);
            PartTimeEmployee pEmp1 = new PartTimeEmployee("Grant", "Ward", 8.5m, 10);
            PartTimeEmployee pEmp2 = new PartTimeEmployee("Daisy", "Johnson", 9.5m, 10);

            employees.Add(fEmp1);
            employees.Add(fEmp2);
            employees.Add(pEmp1);
            employees.Add(pEmp2);

            ckbxFullTime.IsChecked = true;
            ckbxPartTime.IsChecked = true;

            CkBxIsChecked();
        }

        private void CkBxIsChecked()
        {
            if (ckbxFullTime.IsChecked == true && ckbxPartTime.IsChecked == true)
            {
                employees.Sort();
                lbxEmployees.ItemsSource = employees;
            }
            else if (ckbxFullTime.IsChecked == true && ckbxPartTime.IsChecked == false)
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
            else if (ckbxPartTime.IsChecked == true && ckbxFullTime.IsChecked == false)
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
            else if(ckbxFullTime.IsChecked == false && ckbxPartTime.IsChecked == false)
            {
                lbxEmployees.ItemsSource = null;
            }
        }

        private void ckbxFullTime_Checked(object sender, RoutedEventArgs e)
        {
            CkBxIsChecked();
        }

        private void ckbxFullTime_Unchecked(object sender, RoutedEventArgs e)
        {
            CkBxIsChecked();
        }

        private void ckbxPartTime_Checked(object sender, RoutedEventArgs e)
        {
            CkBxIsChecked();
        }

        private void ckbxPartTime_Unchecked(object sender, RoutedEventArgs e)
        {
            CkBxIsChecked();
        }

        private void lbxEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Employee selectedEmployee = lbxEmployees.SelectedItem as Employee;

            if(selectedEmployee != null)
            {
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

        private void btnAdd_Click(object sender, RoutedEventArgs e)//FIX HERE
        {
            if(tbxFirstName.Text != "First Name..." && tbxFirstName.Text != "" && 
                tbxLastName.Text != "Last Name..." && tbxLastName.Text != "" &&
                (rbtnFullTime.IsChecked != false && rbtnPartTime.IsChecked == false))
            {
                string firstName = "";
                string lastName = "";
                if (rbtnFullTime.IsChecked == true)
                {
                    firstName = tbxFirstName.Text;
                    lastName = tbxLastName.Text;
                    decimal salary = Convert.ToDecimal(tbxSalary.Text);//FIX HERE
                    FullTimeEmployee newFTEmp = new FullTimeEmployee(firstName, lastName, salary);
                    employees.Add(newFTEmp);
                    lbxEmployees.ItemsSource = null;
                    CkBxIsChecked();
                }
                else if (rbtnPartTime.IsChecked == true)
                {
                    firstName = tbxFirstName.Text;
                    lastName = tbxLastName.Text;
                    double hoursWorked = Convert.ToDouble(tbxHoursWorked.Text);//FIX HERE
                    decimal hourlyRate = Convert.ToDecimal(tbxHourlyRate.Text);
                    PartTimeEmployee newPTEmp = new PartTimeEmployee(firstName, lastName, hourlyRate, hoursWorked);
                    employees.Add(newPTEmp);
                    lbxEmployees.ItemsSource = null;
                    CkBxIsChecked();
                }
                else
                {
                    firstName = tbxFirstName.Text;
                    lastName = tbxLastName.Text;
                    if(rbtnFullTime.IsChecked == true)
                    {
                        FullTimeEmployee newFTEmp = new FullTimeEmployee(firstName, lastName);
                        employees.Add(newFTEmp);
                        lbxEmployees.ItemsSource = null;//FIX HERE
                        CkBxIsChecked();
                    }
                    else if (rbtnPartTime.IsChecked == true)
                    {
                        PartTimeEmployee newPTEmp = new PartTimeEmployee(firstName, lastName);
                        employees.Add(newPTEmp);
                        lbxEmployees.ItemsSource = null;//FIX HERE
                        CkBxIsChecked();
                    }
                }
            }
            else
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

        private void tbxFirstName_GotFocus(object sender, RoutedEventArgs e)
        {
            tbxFirstName.Clear();
            tbxFirstName.Background = Brushes.White;
        }

        private void tbxLastName_GotFocus(object sender, RoutedEventArgs e)
        {
            tbxLastName.Clear();
            tbxLastName.Background = Brushes.White;
        }

        private void tbxSalary_GotFocus(object sender, RoutedEventArgs e)
        {
            tbxSalary.Clear();
        }

        private void tbxHoursWorked_GotFocus(object sender, RoutedEventArgs e)
        {
            tbxHoursWorked.Clear();
        }

        private void tbxHourlyRate_GotFocus(object sender, RoutedEventArgs e)
        {
            tbxHourlyRate.Clear();
        }

        private void rbtnFullTime_Click(object sender, RoutedEventArgs e)
        {
            tbxHoursWorked.Clear();
            tbxHourlyRate.Clear();
            tbxSalary.Text = "Salary...";
            rbtnFullTime.Background = Brushes.White;
            rbtnPartTime.Background = Brushes.White;
        }

        private void rbtnPartTime_Click(object sender, RoutedEventArgs e)
        {
            tbxSalary.Clear();
            tbxHoursWorked.Text = "Hours Worked...";
            tbxHourlyRate.Text = "Hourly Rate...";
            rbtnFullTime.Background = Brushes.White;
            rbtnPartTime.Background = Brushes.White;
        }
    }
}
