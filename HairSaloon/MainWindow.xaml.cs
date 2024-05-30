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

        private void Human_Click(object sender, RoutedEventArgs e) //переход на страницу со списком людей
        {
            HumanWindow humanWindow = new HumanWindow();  //для открытия окна создаем его объект
            this.Close(); //закрывает уже открытое окно
            humanWindow.Show(); //открывает новое окно
        }

        private void Order_Click(object sender, RoutedEventArgs e)//переход на страницу с заказами
        {
            OrderWindow orderWindow = new OrderWindow();  //для открытия окна создаем его объект
            this.Close(); //закрывает уже открытое окно
            orderWindow.Show(); //открывает новое окно
        }

        private void Service_Click(object sender, RoutedEventArgs e) //переход на страницу услуг
        {
            ServiceWindow serviceWindow = new ServiceWindow(); //для открытия окна создаем его объект
            this.Close(); //закрывает уже открытое окно
            serviceWindow.Show(); //открывает новое окно
        }

        private void Employee_Click(object sender, RoutedEventArgs e) //переход на страницу сотрудников
        {
            EmployeeWindow employeeWindow = new EmployeeWindow();  //для открытия окна создаем его объект
            this.Close(); //закрывает уже открытое окно
            employeeWindow.Show(); //открывает новое окно
        }
    }
}