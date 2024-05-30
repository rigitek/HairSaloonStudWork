﻿using HairSaloon.Models;
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
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        HairSaloonContext db = new HairSaloonContext();

        public OrderWindow()
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
                DataContext = db.Orders.Local.ToObservableCollection().Where(x => x.State == true);
            }
            // вывод по состоянию невыполненные
            if (Complete.SelectedIndex == 1)
            {
                DataContext = db.Orders.Local.ToObservableCollection().Where(x => x.State == false);
            }
            // вывод по состоянию все
            if (Complete.SelectedIndex == 2)
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
            Order? order = ordersList.SelectedItem as Order;
            if (order is null) return;

            AddOrderWindow AddOrderWindow = new AddOrderWindow(new Order
            {
                Id = order.Id,
                Date = order.Date,
                Time = order.Time,
                WashHair=order.WashHair,
                State=order.State,
                Service=order.Service,
                Human=order.Human,
                Employee=order.Employee
            });


            if (AddOrderWindow.ShowDialog() == true)
            {
                // получаем измененный объект
                order = db.Orders.Find(AddOrderWindow.Order.Id);
                if (order != null)
                {
                    order.Date = AddOrderWindow.Order.Date;
                    order.Time = AddOrderWindow.Order.Time;
                    order.WashHair = AddOrderWindow.Order.WashHair;
                    order.State = AddOrderWindow.Order.State;
                    

                    db.SaveChanges();
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
            db.Orders.Remove(order);
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
