
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
        public Studens()
        {
            InitializeComponent();
            Loaded += Studens_Loaded;

            comboBox.Items.Add("Name");
            comboBox.Items.Add("Surname");

            comboBox.SelectedItem = "Name";

        }

        private async void Studens_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> CourseNames = new List<string>();

            var students = await Task.Run(() => AppData.db.Students.ToList());

            await Dispatcher.InvokeAsync(() => { DataGrid.ItemsSource = students; });

            foreach (var item in AppData.db.Courses) CourseNames.Add(item.Name);

            (DataGrid.Columns[4] as DataGridComboBoxColumn).ItemsSource = CourseNames;
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
    }
}
