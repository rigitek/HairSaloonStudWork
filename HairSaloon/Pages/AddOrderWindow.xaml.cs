using HairSaloon.Models;
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

            Order = order;
            humansComboBox.SelectedIndex = Employee.Human.Id - 1;
            humansComboBox.IsEnabled = false;
            DataContext = Order;
        }

        public AddOrderWindow()
        {
            InitializeComponent();
            this.Loaded += AddEmployeeWindow_Loaded;
        }

        private void AddEmployeeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            db.Humans.Load();
            humans = db.Humans.ToList();
            humansComboBox.ItemsSource = humans;
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
