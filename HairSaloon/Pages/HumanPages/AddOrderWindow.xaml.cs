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

namespace HairSaloon.Pages.HumanPages
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
        //через конструктор получаем объект 
        public AddOrderWindow(Order order)
        {
            InitializeComponent();
            this.Loaded += AddEmployeeWindow_Loaded;

            //меняем заголовок окна
            TitleName.Text = "Изменение записи";

            Order = order;

            //присваиваем комбобоксу записанное в бд значение для отображения
            employeesComboBox.SelectedIndex = Order.Employee.Id - 1;
            humansComboBox.SelectedIndex = Order.Human.Id - 1;
            servicesComboBox.SelectedIndex = Order.Service.Id - 1;

            //выключаем возможность взаимодействия с комбобокс
            humansComboBox.IsEnabled = false;
            employeesComboBox.IsEnabled = false;
            servicesComboBox.IsEnabled = false;

            //устанавливаем объект в качестве контекста данных
            DataContext = Order;
        }

        public AddOrderWindow()
        {
            InitializeComponent();
            this.Loaded += AddEmployeeWindow_Loaded;

            //меняем заголовок окна
            TitleName.Text = "Новая заявка";


        }

        private void AddEmployeeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //загружаем данные из бд
            db.Humans.Load();
            db.Employees.Load();
            db.Services.Load();

            //присваиваем данные из бд
            humans = db.Humans.ToList();
            employees = db.Employees.ToList();
            services = db.Services.ToList();

            //передаем данные для отображения в комбобокс
            humansComboBox.ItemsSource = humans;
            employeesComboBox.ItemsSource = employees;
            servicesComboBox.ItemsSource = services;
        }

        void Accept_Click(object sender, RoutedEventArgs e)
        {
            //если order не равен 0, то закрывается окно с результатом true
            if (Order != null) DialogResult = true;
            else
            {
                //получаем выбранные объекты
                Human human = humansComboBox.SelectedItem as Human;
                Employee employee = employeesComboBox.SelectedItem as Employee;
                Service service = servicesComboBox.SelectedItem as Service;

                //проверяем что все объекты получены
                if (human == null) return;
                if (employee == null) return;
                if (service == null) return;

                //создаем новый обьект и заполняем данными введенными в окне
                Order order = new Order
                {
                    Date = Date.SelectedDate.Value,
                    Time = TimeBox.Text,
                    WashHair = WashHair.IsChecked.Value,
                    State = stateComboBox.Text,
                    Human = human,
                    Employee = employee,
                    Service = service
                };

                //прикрепляем объекты к текущему контексту данных
                db.Humans.Attach(human);
                db.Employees.Attach(employee);
                db.Services.Attach(service);
                //добавляем новый объект в бд
                db.Orders.Add(order);
                //сохраняем изменения в бд
                db.SaveChanges();

                DialogResult = true;
            }
        }
    }
}
