using HairSaloon.Pages;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HairSaloon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //переход на страницу со списком людей
        private void Human_Click(object sender, RoutedEventArgs e) 
        {
            //для открытия окна создаем его объект
            HumanWindow humanWindow = new HumanWindow();
            //закрывает уже открытое окно
            this.Close();
            //открывает новое окно
            humanWindow.Show(); 
        }

        //переход на страницу с заказами
        private void Order_Click(object sender, RoutedEventArgs e)
        {
            //для открытия окна создаем его объект
            OrderWindow orderWindow = new OrderWindow();
            //закрывает уже открытое окно
            this.Close();
            //открывает новое окно
            orderWindow.Show();
        }

        //переход на страницу услуг
        private void Service_Click(object sender, RoutedEventArgs e) 
        {
            //для открытия окна создаем его объект
            ServiceWindow serviceWindow = new ServiceWindow();
            //закрывает уже открытое окно
            this.Close();
            //открывает новое окно
            serviceWindow.Show();
        }

        //переход на страницу сотрудников
        private void Employee_Click(object sender, RoutedEventArgs e) 
        {
            //для открытия окна создаем его объект
            EmployeeWindow employeeWindow = new EmployeeWindow();
            //закрывает уже открытое окно
            this.Close();
            //открывает новое окно
            employeeWindow.Show(); 
        }
    }
}