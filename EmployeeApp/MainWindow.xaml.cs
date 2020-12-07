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
        ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
        ObservableCollection<FullTimeEmployee> fullTimeEmployees = new ObservableCollection<FullTimeEmployee>();
        ObservableCollection<PartTimeEmployee> partTimeEmployees = new ObservableCollection<PartTimeEmployee>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FullTimeEmployee fEmp1 = new FullTimeEmployee("Leopold", "Fitz", 32000);
            FullTimeEmployee fEmp2 = new FullTimeEmployee("Gemma", "Simmons", 35000);
            PartTimeEmployee pEmp1 = new PartTimeEmployee("Grant", "Ward", 12, 10);
            PartTimeEmployee pEmp2 = new PartTimeEmployee("Daisy", "Johnson", 14, 12);

            employees.Add(fEmp1);
            employees.Add(fEmp2);
            employees.Add(pEmp1);
            employees.Add(pEmp2);
            fullTimeEmployees.Add(fEmp1);
            fullTimeEmployees.Add(fEmp2);
            partTimeEmployees.Add(pEmp1);
            partTimeEmployees.Add(pEmp2);
        }
    }
}
