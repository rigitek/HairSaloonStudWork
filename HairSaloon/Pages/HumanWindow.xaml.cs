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

namespace HairSaloon.Pages
{
    /// <summary>
    /// Логика взаимодействия для HumanWindow.xaml
    /// </summary>
    public partial class HumanWindow : Window
    {
        HairSaloonContext db = new HairSaloonContext();
        
        public HumanWindow()
        {
            InitializeComponent();
            // запускаем метод при открытии окна
            this.Loaded += HumanWindow_Loaded;
        }

        private void HumanWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //загружаем данные из бд
            db.Humans.Load();
            // устанавливаем данные в качестве контекста
            DataContext = db.Humans.Local.ToObservableCollection();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            //создаем обьект нового окна с созданием нового обьекта для записи в бд
            AddHumanWindow AddHumanWindow = new AddHumanWindow(new Human());

            //если открытое окно завершилось с true
            if (AddHumanWindow.ShowDialog() == true)
            {
                Human Human = AddHumanWindow.Human;
                db.Humans.Add(Human);
                db.SaveChanges();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            //получаем выделенный объект
            Human? human = humansList.SelectedItem as Human;
            if (human is null) return;

            //передача данных выбранного обьекта в окно
            AddHumanWindow AddHumanWindow = new AddHumanWindow(new Human
            {
                Id = human.Id,
                FirstName=human.FirstName,
                LastName=human.LastName,
                PhoneNumber=human.PhoneNumber
            }) ;


            if (AddHumanWindow.ShowDialog() == true)
            {
                // получаем измененный объект
                human = db.Humans.Find(AddHumanWindow.Human.Id);
                //если объект найдет
                if (human != null)
                {
                    human.FirstName = AddHumanWindow.Human.FirstName;
                    human.LastName = AddHumanWindow.Human.LastName;
                    human.PhoneNumber = AddHumanWindow.Human.PhoneNumber;

                    //сохраняем изменения в бд
                    db.SaveChanges();
                    //обновляем список 
                    humansList.Items.Refresh();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Human? human= humansList.SelectedItem as Human;
            // если ни одного объекта не выделено, выходим
            if (human is null) return;
            //удаляем выделенный обьект из бд
            db.Humans.Remove(human);
            // сохраняем изменения в бд
            db.SaveChanges();
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
