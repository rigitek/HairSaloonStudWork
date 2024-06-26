﻿using HairSaloon.Models;
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
    /// Логика взаимодействия для AddHumanWindow.xaml
    /// </summary>
    public partial class AddHumanWindow : Window
    {
        public Human Human { get; set; }
        //через конструктор получаем объект 
        public AddHumanWindow(Human human)
        {
            InitializeComponent();
            
            Human= human;
            //устанавливаем объект в качестве контекста данных
            DataContext = Human;
        }

        //передает результат при закрытии окна
        void Accept_Click(object sender, RoutedEventArgs e)
        {

            DialogResult = true;
        }
    }
}
