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

namespace HairSaloon.Pages.AdminPages
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

        //добавление
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            //создаем обьект нового окна с созданием нового обьекта для записи в бд
            AddAddictServiceWindow AddAddictServiceWindow = new AddAddictServiceWindow(new AddictService());

            //если открытое окно завершилось с true
            if (AddAddictServiceWindow.ShowDialog() == true)
            {
                //создаем обьект для записи в бд и передаем данные введенные в окне
                AddictService AddictService = AddAddictServiceWindow.AddictService;
                //добавляем новый обьект в бд
                db.AddictServices.Add(AddictService);
                //сохраняем изменения в бд
                db.SaveChanges();
            }
        }

        //редактирование
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            //получаем выделенный объект
            AddictService? addictService = addictServicesList.SelectedItem as AddictService;
            // если ни одного объекта не выделено, выходим
            if (addictService is null) return;

            //передача данных выбранного обьекта в окно
            AddAddictServiceWindow AddAddictServiceWindow = new AddAddictServiceWindow(new AddictService
            {
                Id = addictService.Id,
                Title = addictService.Title
            });


            if (AddAddictServiceWindow.ShowDialog() == true)
            {
                // находим объект в бд в котором будем обновлять данные
                addictService = db.AddictServices.Find(AddAddictServiceWindow.AddictService.Id);
                //если объект найдет
                if (addictService != null)
                {
                    //новые данные записываются вместо старых
                    addictService.Title = AddAddictServiceWindow.AddictService.Title;

                    //сохраняем изменения в бд
                    db.SaveChanges();
                    //обновляем список 
                    addictServicesList.Items.Refresh();
                }
            }
        }

        //удаление
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            AddictService? addictService = addictServicesList.SelectedItem as AddictService;
            // если ни одного объекта не выделено, выходим
            if (addictService is null) return;
            //удаляем выделенный обьект из бд
            db.AddictServices.Remove(addictService);
            // сохраняем изменения в бд
            db.SaveChanges();
        }

        //возврат обратно в меню
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
