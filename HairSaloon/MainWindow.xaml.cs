using HairSaloon.Models;
using HairSaloon.Pages;
using HairSaloon.Pages.EmployeePages;
using HairSaloon.Pages.HumanPages;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
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
        HairSaloonContext db = new HairSaloonContext();
        
        List<Human> humans { get; set; }
        List<Employee> employees  { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            GlobalVar.Human = new Human();
            

            db.Humans.Load();

           humans=db.Humans.ToList();
            employees = db.Employees.ToList();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (Login.Text.Length > 0) // проверяем введён ли логин     
            {
                if (Password.Password.Length > 0) // проверяем введён ли пароль         
                {   
                    // ищем в базе данных пользователя с такими данными         
                    var db_user = humans.Where(x => x.Login == Login.Text && x.Password == Password.Password);

                    if (db_user.Count() > 0) // если такая запись существует       
                    {
                        GlobalVar.Human = humans.FirstOrDefault(x => x.Login == Login.Text && x.Password == Password.Password);

                        var db_employee = employees.Where(x=>x.Human==GlobalVar.Human);

                        if(db_employee.Count()>0)
                        {
                            GlobalVar.Human.Employee=employees.Where(x=>x.Human==GlobalVar.Human).FirstOrDefault();
                        }

                        if (GlobalVar.Human != null && GlobalVar.Human.Employee==null)
                        {
                            HumanMainMenuWindow humanMainMenuWindow = new HumanMainMenuWindow();
                            //закрывает уже открытое окно
                            this.Close();
                            //открывает новое окно
                            humanMainMenuWindow.Show();
                        }
                       else if (GlobalVar.Human.Employee.WomenHaircut == true || GlobalVar.Human.Employee.ManHaircut == true)
                        {
                            //для открытия окна создаем его объект
                            EmployeeMainMenuWindow employeeMainMenuWindow = new EmployeeMainMenuWindow();
                            //закрывает уже открытое окно
                            this.Close();
                            //открывает новое окно
                            employeeMainMenuWindow.Show();
                        }
                       else if (GlobalVar.Human.Employee.Admin == true)
                        {
                            //для открытия окна создаем его объект
                            AdminMainMenuWindow adminMainMenuWindow = new AdminMainMenuWindow();
                            //закрывает уже открытое окно
                            this.Close();
                            //открывает новое окно
                            adminMainMenuWindow.Show();
                        }
                        
                        
                    }
                    else MessageBox.Show("Пользователь не найден"); // выводим ошибку 
                }
                else MessageBox.Show("Введите пароль"); // выводим ошибку    
            }
            else MessageBox.Show("Введите логин"); // выводим ошибку 
        }
    }
}