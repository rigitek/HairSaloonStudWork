using HairSaloon.Models;
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
    /// Логика взаимодействия для AddServiceWindow.xaml
    /// </summary>
    public partial class AddServiceWindow : Window
    {
        public Service Service;
        public AddServiceWindow(Service service)
        {
            InitializeComponent();
            Service = service;
            DataContext = Service;
        }

        void Accept_Click(object sender, RoutedEventArgs e)
        {

            DialogResult = true;
        }
    }
}
