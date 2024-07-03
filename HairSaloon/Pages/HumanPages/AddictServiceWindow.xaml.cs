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
    /// Логика взаимодействия для AddictServiceWindow.xaml
    /// </summary>
    public partial class AddictServiceWindow : Window
    {
        HairSaloonContext db = new HairSaloonContext();

        public AddictServiceWindow()
        {
            InitializeComponent();
            // запускаем метод при открытии окна
            this.Loaded += AddictServiceWindow_Loaded;
        }

        // при загрузке окна 
        private void AddictServiceWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //загружаем данные услуг из бд
            db.AddictServices.Load();
            // устанавливаем данные в качестве контекста
            DataContext = db.AddictServices.Local.ToObservableCollection();
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
