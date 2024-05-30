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

        //добавление
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            //создаем обьект нового окна с созданием нового обьекта для записи в бд
            AddServiceWindow AddServiceWindow = new AddServiceWindow(new Service());

            //если открытое окно завершилось с true
            if (AddServiceWindow.ShowDialog() == true)
            {
                //создаем обьект для записи в бд и передаем данные введенные в окне
                Service Service = AddServiceWindow.Service;
                //добавляем новый обьект в бд
                db.Services.Add(Service);
                //сохраняем изменения в бд
                db.SaveChanges();
            }
        }

        //редактирование
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            //получаем выделенный объект
            Service? service = servicesList.SelectedItem as Service;
            // если ни одного объекта не выделено, выходим
            if (service is null) return;

            //передача данных выбранного обьекта в окно
            AddServiceWindow AddServiceWindow = new AddServiceWindow(new Service
            {
                Id = service.Id,
                Title = service.Title,
                Price = service.Price
            });


            if (AddServiceWindow.ShowDialog() == true)
            {
                // находим объект в бд в котором будем обновлять данные
                service = db.Services.Find(AddServiceWindow.Service.Id);
                //если объект найдет
                if (service != null)
                {
                    //новые данные записываются вместо старых
                    service.Title = AddServiceWindow.Service.Title;
                    service.Price = AddServiceWindow.Service.Price;

                    //сохраняем изменения в бд
                    db.SaveChanges();
                    //обновляем список 
                    servicesList.Items.Refresh();
                }
            }
        }

        //удаление
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Service? service = servicesList.SelectedItem as Service;
            // если ни одного объекта не выделено, выходим
            if (service is null) return;
            //удаляем выделенный обьект из бд
            db.Services.Remove(service);
            // сохраняем изменения в бд
            db.SaveChanges(); 
        }

        //возврат обратно в меню
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
