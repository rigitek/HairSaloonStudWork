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
    /// Логика взаимодействия для AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        HairSaloonContext db = new HairSaloonContext();
        List<Human> humans;
        public Employee Employee { get; set; }
        public AddEmployeeWindow(Employee employee)
        {
            InitializeComponent();
            this.Loaded += AddEmployeeWindow_Loaded;
            Human human = humansComboBox.SelectedItem as Human;
            Employee = employee;
            DataContext = Employee;
            
        }

        private void AddEmployeeWindow_Loaded(object sender, RoutedEventArgs e)
        {

            db.Humans.Load();
            humans = db.Humans.ToList();
            humansComboBox.ItemsSource= humans;

        }

        void Accept_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
