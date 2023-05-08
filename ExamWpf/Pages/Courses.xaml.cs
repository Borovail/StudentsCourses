
using ExamWpf.Model;
using System;
using System.Collections.Generic;
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
        List<string> MyListBoxItems;
        public Courses()
        {
            InitializeComponent();

            Loaded += Courses_Loaded;

            MyListBoxItems = new List<string>() { "dffsd", "fdsf", "fdsf" };

            comboBox.Items.Add("Teacher");
            comboBox.Items.Add("Name");

            comboBox.SelectedItem= "Name";

        }

        private async void Courses_Loaded(object sender, RoutedEventArgs e)
        {
            var courses = await Task.Run(() => AppData.db.Courses.ToList());

            await Dispatcher.InvokeAsync(() => { DataGrid.ItemsSource = courses; });
        }

        private async void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {
                if (MessageBox.Show("Are you sure?", "Remove", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    AppData.db.Courses.Remove(DataGrid.SelectedItem as Model.Courses);

                    await AppData.db.SaveChangesAsync();
                }
            }
        }

        private async void Update_btn_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {
                AppData.db.Courses.AddOrUpdate(DataGrid.SelectedItem as Model.Courses);

                await AppData.db.SaveChangesAsync();
            }
        }

        private async void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {
                var currentCourse = DataGrid.SelectedItem as Model.Courses;

                var course = new Model.Courses() { Name = currentCourse.Name, Teacher= currentCourse.Teacher };

                AppData.db.Courses.Add(course);

                await AppData.db.SaveChangesAsync();
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
    }
}
