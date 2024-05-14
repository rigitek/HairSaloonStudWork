using HairSaloon.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        HairSaloonContext db = new HairSaloonContext();

        //List<Employee> employees;

        public EmployeeWindow()
        {
            InitializeComponent();
            this.Loaded += EmployeeWindow_Loaded;
        }

        private void EmployeeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            db.Humans.Load();
            db.Employees.Load();
            DataContext = db.Employees.Local.ToObservableCollection();
           //employeesList.ItemsSource = db.Employees;
          
            //employees = db.Employees.ToList();
           // employeesList.ItemsSource= employees;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

            //AddEmployeeWindow AddEmployeeWindow = new AddEmployeeWindow(new Employee());

            AddEmployeeWindow AddEmployeeWindow = new AddEmployeeWindow();
            if (AddEmployeeWindow.ShowDialog() == true)
            {
                db.Employees.Load();

            }

            //if (AddEmployeeWindow.ShowDialog() == true)
            //{

            //    db.Humans.Load();
            //    Employee Employee = AddEmployeeWindow.Employee;
            //    db.Humans.Attach(Employee.Human);
            //    db.Employees.Add(Employee);
            //    db.SaveChanges();
            //}
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Employee? employee = employeesList.SelectedItem as Employee;
            if (employee is null) return;

            AddEmployeeWindow AddEmployeeWindow = new AddEmployeeWindow(new Employee
            {
                Id = employee.Id,
                WomenHaircut = employee.WomenHaircut,
                ManHaircut = employee.ManHaircut,
                Admin = employee.Admin,
                Human = employee.Human
            });


            if (AddEmployeeWindow.ShowDialog() == true)
            {
                // получаем измененный объект
                employee = db.Employees.Find(AddEmployeeWindow.Employee.Id);
                if (employee != null)
                {
                    employee.WomenHaircut = AddEmployeeWindow.Employee.WomenHaircut;
                    employee.ManHaircut = AddEmployeeWindow.Employee.ManHaircut;
                    employee.Admin = AddEmployeeWindow.Employee.Admin;
                    
                    db.SaveChanges();
                    employeesList.Items.Refresh();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Employee? employee = employeesList.SelectedItem as Employee;
            // если ни одного объекта не выделено, выходим
            if (employee is null) return;
            db.Employees.Remove(employee);
            db.SaveChanges();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }
    }
}
