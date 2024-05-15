using HairSaloon.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace HairSaloon.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddOrderWindow.xaml
    /// </summary>
    public partial class AddOrderWindow : Window
    {
        HairSaloonContext db = new HairSaloonContext();
        List<Human> humans;
        List<Employee> employees;
        List<Service> services;
        public Order Order { get; set; }

        public AddOrderWindow(Order order)
        {
            InitializeComponent();
            this.Loaded += AddEmployeeWindow_Loaded;

            Title.Text = "Изменение заявки";

            Order = order;

            employeesComboBox.SelectedIndex = Order.Employee.Id - 1;
            humansComboBox.SelectedIndex = Order.Human.Id-1;
            servicesComboBox.SelectedIndex = Order.Service.Id - 1;

            humansComboBox.IsEnabled = false;
            employeesComboBox.IsEnabled = false;
            servicesComboBox.IsEnabled = false;

            DataContext = Order;
        }

        public AddOrderWindow()
        {
            InitializeComponent();
            this.Loaded += AddEmployeeWindow_Loaded;

            Title.Text = "Новая заявка";
        }

        private void AddEmployeeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            db.Humans.Load();
            db.Employees.Load();
            db.Services.Load();

            humans = db.Humans.ToList();
            employees = db.Employees.ToList();
            services = db.Services.ToList();

            humansComboBox.ItemsSource = humans;
            employeesComboBox.ItemsSource = employees;
            servicesComboBox.ItemsSource = services;

        }

        void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (Order != null) DialogResult = true;
            else
            {
                Human human = humansComboBox.SelectedItem as Human;
                Employee employee = employeesComboBox.SelectedItem as Employee;
                Service service = servicesComboBox.SelectedItem as Service;
               

                if (human == null) return;
                if (employee == null) return;
                if (service == null) return;

                Order order= new Order
                {
                    Date = Date.SelectedDate.Value,
                    Time = TimeBox.Text,
                    WashHair = WashHair.IsChecked.Value,
                    State= false,
                    Human = human,
                    Employee=employee,
                    Service=service
                };

                db.Humans.Attach(human);
                db.Employees.Attach(employee);
                db.Services.Attach(service);
                db.Orders.Add(order);
                db.SaveChanges();

                DialogResult = true;
            }
        }
    }
}
