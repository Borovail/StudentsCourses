﻿using ExamWpf.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExamWpf.Pages
{
    /// <summary>
    /// Логика взаимодействия для Courses.xaml
    /// </summary>
    public partial class Courses : Page
    {
        List<string> MyListBoxItems;
        public Courses()
        {
            InitializeComponent();
            Loaded += Courses_Loaded;

        }

        private async void Courses_Loaded(object sender, RoutedEventArgs e)
        {
            var courses = await Task.Run(() => AppData.db.Students.ToList());

            await Dispatcher.InvokeAsync(() => { DataGrid.ItemsSource = courses; });
  
        }
    }
}
