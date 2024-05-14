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
            this.Loaded += HumanWindow_Loaded;

        }

        private void HumanWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
            db.Humans.Load();
            DataContext = db.Humans.Local.ToObservableCollection();
            
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddHumanWindow AddHumanWindow = new AddHumanWindow(new Human());

            //AddHumanWindow.Show();
            if (AddHumanWindow.ShowDialog() == true)
            {
                Human Human = AddHumanWindow.Human;
                db.Humans.Add(Human);
                db.SaveChanges();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Human? human = humansList.SelectedItem as Human;
            if (human is null) return;

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
                if (human != null)
                {
                    human.FirstName = AddHumanWindow.Human.FirstName;
                    human.LastName = AddHumanWindow.Human.LastName;
                    human.PhoneNumber = AddHumanWindow.Human.PhoneNumber;
                    db.SaveChanges();
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
            db.Humans.Remove(human);
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
