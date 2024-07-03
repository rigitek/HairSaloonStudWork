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

namespace HairSaloon.Pages.EmployeePages
{
    /// <summary>
    /// Логика взаимодействия для EditOrderWindow.xaml
    /// </summary>
    public partial class EditOrderWindow : Window
    {
        HairSaloonContext db = new HairSaloonContext();

        List<Human> humans;
        List<Employee> employees;
        List<Service> services;
        List<AddictService> addictServices;
        public Order Order { get; set; }
        //через конструктор получаем объект 
        public EditOrderWindow(Order order)
        {
            InitializeComponent();
            this.Loaded += AddEmployeeWindow_Loaded;

            //меняем заголовок окна
            TitleName.Text = "Изменение заявки";

            Order = order;

            //присваиваем комбобоксу записанное в бд значение для отображения
            employeesComboBox.SelectedIndex = Order.Employee.Id - 1;
            humansComboBox.SelectedIndex = Order.Human.Id - 1;
            servicesComboBox.SelectedIndex = Order.Service.Id - 1;
            addictServicesComboBox.SelectedIndex = Order.AddictService.Id - 1;
            stateComboBox.Text = Order.State;

            //выключаем возможность взаимодействия с комбобокс
            humansComboBox.IsEnabled = false;
            employeesComboBox.IsEnabled = false;
            servicesComboBox.IsEnabled = false;
            addictServicesComboBox.IsEnabled = false;
            sliderRate.IsEnabled = false;
            Comment.IsEnabled = false;

            //устанавливаем объект в качестве контекста данных
            DataContext = Order;
        }

        public EditOrderWindow()
        {
            InitializeComponent();
            this.Loaded += AddEmployeeWindow_Loaded;

            //меняем заголовок окна
            TitleName.Text = "Новая заявка";

            sliderRate.IsEnabled = false;
            Comment.IsEnabled = false;
        }

        private void AddEmployeeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //загружаем данные из бд
            db.Humans.Load();
            db.Employees.Load();
            db.Services.Load();
            db.Rates.Load();
            db.AddictServices.Load();

            //присваиваем данные из бд
            humans = db.Humans.ToList();
            employees = db.Employees.ToList();
            services = db.Services.ToList();
            addictServices = db.AddictServices.ToList();


            //передаем данные для отображения в комбобокс
            humansComboBox.ItemsSource = humans;
            employeesComboBox.ItemsSource = employees;
            servicesComboBox.ItemsSource = services;
            addictServicesComboBox.ItemsSource = addictServices;
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
                AddictService addictService = addictServicesComboBox.SelectedItem as AddictService;

                //проверяем что все объекты получены
                if (human == null) return;
                if (employee == null) return;
                if (service == null) return;
                if (addictService == null) return;



                //создаем новый обьект и заполняем данными введенными в окне
                Order order = new Order
                {
                    Date = Date.SelectedDate.Value,
                    Time = TimeBox.Text,
                    WashHair = WashHair.IsChecked.Value,
                    State = stateComboBox.Text,
                    Human = human,
                    Employee = employee,
                    Service = service,
                    AddictService = addictService
                };

                //прикрепляем объекты к текущему контексту данных
                db.Humans.Attach(human);
                db.Employees.Attach(employee);
                db.Services.Attach(service);
                db.AddictServices.Attach(addictService);
                //добавляем новый объект в бд
                db.Orders.Add(order);
                //сохраняем изменения в бд
                db.SaveChanges();

                DialogResult = true;
            }
        }
    }
}
