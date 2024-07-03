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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HairSaloon.Pages.EmployeePages
{
    /// <summary>
    /// Логика взаимодействия для OrdersWindow.xaml
    /// </summary>
    public partial class OrdersWindow : Window
    {
        HairSaloonContext db = new HairSaloonContext();

        public OrdersWindow()
        {
            InitializeComponent();
            // запускаем метод при открытии окна
            this.Loaded += OrderWindow_Loaded;
        }

        private void OrderWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //загружаем данные из бд
            db.Humans.Load();
            db.Employees.Load();
            db.Services.Load();
            db.Orders.Load();
            db.AddictServices.Load();
            db.Rates.Load();
            // устанавливаем данные в качестве контекста
            DataContext = db.Orders.Local.ToObservableCollection().Where(x => x.Employee.Id == GlobalVar.Human.Employee.Id);

            EditButton.IsEnabled = false;
        }

        private void Complete_Changed(object sender, RoutedEventArgs e)
        {
            // вывод по состоянию выполненные
            if (Complete.SelectedIndex == 0)
            {
                DataContext = db.Orders.Local.ToObservableCollection().Where(x => x.State == "Выполнено" && x.Employee.Id == GlobalVar.Human.Employee.Id);
            }
            // вывод по состоянию невыполненные
            if (Complete.SelectedIndex == 1)
            {
                DataContext = db.Orders.Local.ToObservableCollection().Where(x => x.State == "Отправлено" && x.Employee.Id == GlobalVar.Human.Employee.Id);
            }
            if (Complete.SelectedIndex == 2)
            {
                DataContext = db.Orders.Local.ToObservableCollection().Where(x => x.State == "Записано" && x.Employee.Id == GlobalVar.Human.Employee.Id);
            }
            // вывод по состоянию все
            if (Complete.SelectedIndex == 3)
            {
                DataContext = db.Orders.Local.ToObservableCollection().Where(x => x.Employee.Id == GlobalVar.Human.Employee.Id) ;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            //создаем обьект нового окна с созданием нового обьекта для записи в бд
            EditOrderWindow EditOrderWindow = new EditOrderWindow();
            //если открытое окно завершилось с true
            if (EditOrderWindow.ShowDialog() == true)
            {
                db.Orders.Load();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            //получаем выделенный объект
            Order? order = ordersList.SelectedItem as Order;
            if (order is null) return;

            //передача данных выбранного обьекта в окно
            EditOrderWindow EditOrderWindow = new EditOrderWindow(new Order
            {
                Id = order.Id,
                Date = order.Date,
                Time = order.Time,
                WashHair = order.WashHair,
                State = order.State,
                Service = order.Service,
                AddictService = order.AddictService,
                Human = order.Human,
                Employee = order.Employee,
                Rate = order.Rate
            });


            if (EditOrderWindow.ShowDialog() == true)
            {
                // получаем измененный объект
                order = db.Orders.Find(EditOrderWindow.Order.Id);
                //если объект найдет
                if (order != null)
                {
                    order.Date = EditOrderWindow.Order.Date;
                    order.Time = EditOrderWindow.Order.Time;
                    order.WashHair = EditOrderWindow.Order.WashHair;
                    order.State = EditOrderWindow.stateComboBox.Text;

                    //сохраняем изменения в бд
                    db.SaveChanges();

                    //обновляем список 
                    db.Orders.Load();
                    ordersList.Items.Refresh();
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            //для открытия окна создаем его объект
            EmployeeMainMenuWindow employeeMainMenuWindow = new EmployeeMainMenuWindow();
            //закрывает уже открытое окно
            this.Close();
            //открываем новое окно
            employeeMainMenuWindow.Show();
        }

        private void ordersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EditButton.IsEnabled = true;
        }
    }
}
