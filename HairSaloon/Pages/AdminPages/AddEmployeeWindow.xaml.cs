
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
       
        public Employee Employee { get; set; }

        //через конструктор получаем объект 
        public AddEmployeeWindow(Employee employee)
        {
            InitializeComponent();
            this.Loaded += AddEmployeeWindow_Loaded;
           
            Employee = employee;
            //присваиваем комбобоксу записанное в бд значение для отображения
            humansComboBox.SelectedItem = Employee.Human;
            //выключаем возможность взаимодействия с комбобокс
            humansComboBox.IsEnabled = false;
            //устанавливаем объект в качестве контекста данных
            DataContext = Employee;
        }

        public AddEmployeeWindow()
        {
            InitializeComponent();
            this.Loaded += AddEmployeeWindow_Loaded;
        }

        private void AddEmployeeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //загружаем данные из бд
            db.Humans.Load();
            db.Employees.Load();

            //присваиваем данные из бд
            humans = db.Humans.ToList();

            //передаем данные для отображения в комбобокс
            humansComboBox.ItemsSource= humans;
        }

        void Accept_Click(object sender, RoutedEventArgs e)
        {
            //если order не равен 0, то закрывается окно с результатом true
            if (Employee != null) DialogResult = true;
            else
            {
                //получаем выбранный объект
                Human human = humansComboBox.SelectedItem as Human;

                //проверяем что объект получен
                if (human == null) return;

                //создаем новый обьект и заполняем данными введенными в окне
                Employee employee = new Employee
                {
                    WomenHaircut = Women.IsChecked.Value,
                    ManHaircut = Man.IsChecked.Value,
                    Admin = Admin.IsChecked.Value,
                    Human = human
                };

                //прикрепляем объекты к текущему контексту данных
                db.Humans.Attach(human);
                //добавляем новый объект в бд
                db.Employees.Add(employee);
                //сохраняем изменения в бд
                db.SaveChanges();

                DialogResult = true;
            }
        }
    }
}
