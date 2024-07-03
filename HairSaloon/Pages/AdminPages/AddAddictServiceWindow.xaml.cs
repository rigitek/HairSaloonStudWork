using HairSaloon.Models;
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
    /// Логика взаимодействия для AddAddictServiceWindow.xaml
    /// </summary>
    public partial class AddAddictServiceWindow : Window
    {
        public AddictService AddictService;
        //через конструктор получаем объект 
        public AddAddictServiceWindow(AddictService addictService)
        {
            InitializeComponent();
            AddictService = addictService;
            //устанавливаем объект в качестве контекста данных
            DataContext = AddictService;
        }

        //передает результат при закрытии окна
        void Accept_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
