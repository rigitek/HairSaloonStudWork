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

        public EmployeeWindow()
        {
            InitializeComponent();
            // запускаем метод при открытии окна
            this.Loaded += EmployeeWindow_Loaded;
        }

        private void EmployeeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //загружаем данные из бд
            db.Humans.Load();
            db.Employees.Load();
            // устанавливаем данные в качестве контекста
            DataContext = db.Employees.Local.ToObservableCollection();

            CheckEmployee();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            //создаем обьект нового окна с созданием нового обьекта для записи в бд
            AddEmployeeWindow AddEmployeeWindow = new AddEmployeeWindow();
            //если открытое окно завершилось с true
            if (AddEmployeeWindow.ShowDialog() == true)
            {
                db.Employees.Load();
                CheckEmployee();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            //получаем выделенный объект
            Employee? employee = employeesList.SelectedItem as Employee;
            if (employee is null) return;

            //передача данных выбранного обьекта в окно
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
                //если объект найдет
                if (employee != null)
                {
                    employee.WomenHaircut = AddEmployeeWindow.Employee.WomenHaircut;
                    employee.ManHaircut = AddEmployeeWindow.Employee.ManHaircut;
                    employee.Admin = AddEmployeeWindow.Employee.Admin;

                    //сохраняем изменения в бд
                    db.SaveChanges();
                    //обновляем список 
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

            CheckEmployee();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            //для открытия окна создаем его объект
            MainWindow mainWindow = new MainWindow();
            //закрывает уже открытое окно
            this.Close();
            //открываем новое окно
            mainWindow.Show();
        }

        //проверка сотрудников на количество
        private void CheckEmployee()
        {
            if(db.Humans.Count()==db.Employees.Count() && db.Humans.Count()!=0) AddButton.IsEnabled = false;
        }
    }
}
