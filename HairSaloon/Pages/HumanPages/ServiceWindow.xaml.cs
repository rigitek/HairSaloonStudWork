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
    /// Логика взаимодействия для ServiceWindow.xaml
    /// </summary>
    public partial class ServiceWindow : Window
    {
        HairSaloonContext db = new HairSaloonContext();

        public ServiceWindow()
        {
            InitializeComponent();
            // запускаем метод при открытии окна
            this.Loaded += ServiceWindow_Loaded;
        }

        // при загрузке окна 
        private void ServiceWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //загружаем данные услуг из бд
            db.Services.Load();
            // устанавливаем данные в качестве контекста
            DataContext = db.Services.Local.ToObservableCollection();
        }

        //возврат обратно в меню
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            //для открытия окна создаем его объект
            HumanMainMenuWindow humanMainMenuWindow = new HumanMainMenuWindow();
            //закрывает уже открытое окно
            this.Close();
            //открываем новое окно
            humanMainMenuWindow.Show();
        }
    }
}
