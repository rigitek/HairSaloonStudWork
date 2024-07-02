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
            // устанавливаем данные в качестве контекста
            DataContext = db.Orders.Local.ToObservableCollection();
        }

        private void Complete_Changed(object sender, RoutedEventArgs e)
        {
            // вывод по состоянию выполненные
            if (Complete.SelectedIndex == 0)
            {
                DataContext = db.Orders.Local.ToObservableCollection().Where(x => x.State == "Выполнено");
            }
            // вывод по состоянию невыполненные
            if (Complete.SelectedIndex == 1)
            {
                DataContext = db.Orders.Local.ToObservableCollection().Where(x => x.State == "Отправлено");
            }
            if (Complete.SelectedIndex == 2)
            {
                DataContext = db.Orders.Local.ToObservableCollection().Where(x => x.State == "Записано");
            }
            // вывод по состоянию все
            if (Complete.SelectedIndex == 3)
            {
                DataContext = db.Orders.Local.ToObservableCollection();
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            //создаем обьект нового окна с созданием нового обьекта для записи в бд
            AddOrderWindow AddOrderWindow = new AddOrderWindow();
            //если открытое окно завершилось с true
            if (AddOrderWindow.ShowDialog() == true)
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
            AddOrderWindow AddOrderWindow = new AddOrderWindow(new Order
            {
                Id = order.Id,
                Date = order.Date,
                Time = order.Time,
                WashHair = order.WashHair,
                State = order.State,
                Service = order.Service,
                Human = order.Human,
                Employee = order.Employee
            });


            if (AddOrderWindow.ShowDialog() == true)
            {
                // получаем измененный объект
                order = db.Orders.Find(AddOrderWindow.Order.Id);
                //если объект найдет
                if (order != null)
                {
                    order.Date = AddOrderWindow.Order.Date;
                    order.Time = AddOrderWindow.Order.Time;
                    order.WashHair = AddOrderWindow.Order.WashHair;
                    order.State = AddOrderWindow.Order.State;

                    //сохраняем изменения в бд
                    db.SaveChanges();
                    //обновляем список 
                    ordersList.Items.Refresh();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Order? order = ordersList.SelectedItem as Order;
            // если ни одного объекта не выделено, выходим
            if (order is null) return;
            //удаляем выделенный обьект из бд
            db.Orders.Remove(order);
            // сохраняем изменения в бд
            db.SaveChanges();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            //для открытия окна создаем его объект
            AdminMainMenuWindow adminMainMenuWindow = new AdminMainMenuWindow();
            //закрывает уже открытое окно
            this.Close();
            //открываем новое окно
            adminMainMenuWindow.Show();
        }
    }
}
