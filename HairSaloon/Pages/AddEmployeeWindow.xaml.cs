using HairSaloon.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HairSaloon.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        HairSaloonContext db = new HairSaloonContext();
        List<Human> humans;
        List<Employee> employees;
        public Employee Employee { get; set; }

        public AddEmployeeWindow(Employee employee)
        {
            InitializeComponent();
            this.Loaded += AddEmployeeWindow_Loaded;
           
            Employee = employee;
            humansComboBox.SelectedItem = Employee.Human;
            humansComboBox.IsEnabled = false;
            DataContext = Employee;
        }

        public AddEmployeeWindow()
        {
            InitializeComponent();
            this.Loaded += AddEmployeeWindow_Loaded;
        }

        private void AddEmployeeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            db.Humans.Load();
            db.Employees.Load();
            

            humans = db.Humans.ToList();
            humansComboBox.ItemsSource= humans;
        }

        void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (Employee != null) DialogResult = true;
            else
            {
                Human human = humansComboBox.SelectedItem as Human;

                if (human == null) return;

                Employee employee = new Employee
                {
                    WomenHaircut = Women.IsChecked.Value,
                    ManHaircut = Man.IsChecked.Value,
                    Admin = Admin.IsChecked.Value,
                    Human = human
                };

                db.Humans.Attach(human);
                db.Employees.Add(employee);
                db.SaveChanges();

                DialogResult = true;
            }
        }
    }
}
