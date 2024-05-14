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

namespace HairSaloon.Pages
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
            this.Loaded += ServiceWindow_Loaded;
        }

        private void ServiceWindow_Loaded(object sender, RoutedEventArgs e)
        {
            db.Services.Load();
            DataContext = db.Services.Local.ToObservableCollection();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddServiceWindow AddServiceWindow = new AddServiceWindow(new Service());

            //AddHumanWindow.Show();
            if (AddServiceWindow.ShowDialog() == true)
            {
                Service Service = AddServiceWindow.Service;
                db.Services.Add(Service);
                db.SaveChanges();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Service? service = servicesList.SelectedItem as Service;
            if (service is null) return;

            AddServiceWindow AddServiceWindow = new AddServiceWindow(new Service
            {
                Id = service.Id,
                Title = service.Title,
                Price = service.Price
            });


            if (AddServiceWindow.ShowDialog() == true)
            {
                // получаем измененный объект
                service = db.Services.Find(AddServiceWindow.Service.Id);
                if (service != null)
                {
                    service.Title = AddServiceWindow.Service.Title;
                    service.Price = AddServiceWindow.Service.Price;

                    db.SaveChanges();
                    servicesList.Items.Refresh();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Service? service = servicesList.SelectedItem as Service;
            // если ни одного объекта не выделено, выходим
            if (service is null) return;
            db.Services.Remove(service);
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
