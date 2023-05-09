
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
    /// Логика взаимодействия для Studens.xaml
    /// </summary>
    public partial class Studens : Page
    {
        public List<string> courseNames { get; set; }
        public Studens()
        {
            InitializeComponent();

            comboBox.Items.Add("Name");
            comboBox.Items.Add("Surname");

            comboBox.SelectedItem = "Name";

          

        }



        private async void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {
                if (MessageBox.Show("Are you sure?", "Remove", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    AppData.db.Students.Remove(DataGrid.SelectedItem as Model.Students);

                    await AppData.db.SaveChangesAsync();
                }
            }
        }

        private async void Update_btn_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {
                AppData.db.Students.AddOrUpdate(DataGrid.SelectedItem as Students);

                await AppData.db.SaveChangesAsync();
            }
        }
        private async void Add_btn_Click(object sender, RoutedEventArgs e)
        {

            if (DataGrid.SelectedItem != null)
            {
                var currentStudent = DataGrid.SelectedItem as Students;

                var student = new Students() { Age = currentStudent.Age, CourseName = currentStudent.CourseName, Name = currentStudent.Name, Surname = currentStudent.Surname };

                AppData.db.Students.Add(student);

                await AppData.db.SaveChangesAsync();
            }
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox.Text == "")
            {
                DataGrid.ItemsSource = AppData.db.Students.ToList();
            }

            if (comboBox.SelectedItem.ToString() == "Name")
                DataGrid.ItemsSource = AppData.db.Students.Where(i => i.Name.Contains(textBox.Text)).ToList();
            else DataGrid.ItemsSource = AppData.db.Students.Where(i => i.Surname.Contains(textBox.Text)).ToList();

        }

        private void Back_bnt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var students = AppData.db.Students.ToList();


            DataGrid.ItemsSource = students;

            DataContext = this;

            courseNames = new List<string>();

            foreach (var item in AppData.db.Courses.ToList())
            {
                courseNames.Add(item.Name);

            }

        }

        private void ComboBox_Selected(object sender, RoutedEventArgs e)
        {
            AppData.db.Students.AddOrUpdate(DataGrid.SelectedItem as Students);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var student =DataGrid.SelectedItem as Students;

            student.CourseName = ((ComboBox)sender).SelectedItem.ToString();

            AppData.db.Students.AddOrUpdate(student);

            ((ComboBox)sender).SelectedItem = student.CourseName;
        }
    }
}
