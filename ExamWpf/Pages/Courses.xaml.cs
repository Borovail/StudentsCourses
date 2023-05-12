﻿
using ExamWpf.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
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
        public Courses()
        {
            InitializeComponent();

            comboBox.Items.Add("Teacher");
            comboBox.Items.Add("Name");

            comboBox.SelectedItem= "Name";

        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var courses = await Task.Run(() => AppData.db.Courses.ToList());

            foreach (var course in courses)
            {
                course.StudentsInCourse = "";

                foreach (var student in AppData.db.Students)
                {
                    if (course.Name == student.CourseName)
                    {
                        course.StudentsInCourse+="("+student.Name + " " + student.Surname + ")   ";
                    }
                }
            }

            await Dispatcher.InvokeAsync(() => { DataGrid.ItemsSource = courses; });

        }


        private async void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (DataGrid.SelectedItem != null)
                {
                    if (MessageBox.Show("Are you sure?", "Remove", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        AppData.db.Courses.Remove(DataGrid.SelectedItem as Model.Courses);

                        await AppData.db.SaveChangesAsync();

                        DataGrid.ItemsSource = AppData.db.Courses.ToList();

                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("You can't delete it", ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private async void Update_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (DataGrid.SelectedItem != null)
                {
                    AppData.db.Courses.AddOrUpdate(DataGrid.SelectedItem as Model.Courses);

                    await AppData.db.SaveChangesAsync();

                    DataGrid.ItemsSource = AppData.db.Courses.ToList();

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("You can't delete it", ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {
                var currentCourse = DataGrid.SelectedItem as Model.Courses;

                if (currentCourse != null)
                {

                    var course = new Model.Courses() { Name = currentCourse.Name, Teacher = currentCourse.Teacher };

                    AppData.db.Courses.Add(course);

                    await AppData.db.SaveChangesAsync();

                    DataGrid.ItemsSource = AppData.db.Courses.ToList();

                }
            }

        }

        private  void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox.Text == "")
            {            
               DataGrid.ItemsSource = AppData.db.Courses.ToList();
            }

            if(comboBox.SelectedItem.ToString()=="Name")
            DataGrid.ItemsSource= AppData.db.Courses.Where(i=>i.Name.Contains(textBox.Text)).ToList();
            else DataGrid.ItemsSource = AppData.db.Courses.Where(i => i.Teacher.Contains(textBox.Text)).ToList();

        }

        private void Back_bnt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            
        }

        private void CovertToCSV_btn_Click(object sender, RoutedEventArgs e)
        {
            PathPage pathPage = new PathPage(DataGrid, ConvertType.Csv);

            NavigationService.Navigate(pathPage);
        }

        private void CovertToExcel_btn_Click(object sender, RoutedEventArgs e)
        {
            PathPage pathPage = new PathPage(DataGrid, ConvertType.Excel);

            NavigationService.Navigate(pathPage);

        }
    }
}
