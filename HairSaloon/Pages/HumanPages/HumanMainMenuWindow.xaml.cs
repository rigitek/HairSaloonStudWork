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
    /// Логика взаимодействия для HumanMainMenuWindow.xaml
    /// </summary>
    public partial class HumanMainMenuWindow : Window
    {
        public HumanMainMenuWindow()
        {
            InitializeComponent();

            welcome.Text = $"Добро пожаловать! {GlobalVar.Human.FirstName} {GlobalVar.Human.LastName}";
        }

        //переход на страницу с заказами
        private void Order_Click(object sender, RoutedEventArgs e)
        {
            //для открытия окна создаем его объект
            OrderWindow orderWindow = new OrderWindow();
            //закрывает уже открытое окно
            this.Close();
            //открывает новое окно
            orderWindow.Show();
        }

        //переход на страницу услуг
        private void Service_Click(object sender, RoutedEventArgs e)
        {
            //для открытия окна создаем его объект
            ServiceWindow serviceWindow = new ServiceWindow();
            //закрывает уже открытое окно
            this.Close();
            //открывает новое окно
            serviceWindow.Show();
        }

        private void AddictService_Click(object sender, RoutedEventArgs e)
        {
            //для открытия окна создаем его объект
            AddictServiceWindow addictServiceWindow = new AddictServiceWindow();
            //закрывает уже открытое окно
            this.Close();
            //открывает новое окно
            addictServiceWindow.Show();
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
    }
}
