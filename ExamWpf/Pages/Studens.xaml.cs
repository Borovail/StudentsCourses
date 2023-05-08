
using ExamWpf.Model;
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
    /// Логика взаимодействия для Studens.xaml
    /// </summary>
    public partial class Studens : Page
    {
        public Studens()
        {
            InitializeComponent();
            Loaded += Studens_Loaded;

        }

        private async void Studens_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> CourseNames = new List<string>();

            var students = await Task.Run(() => AppData.db.Students.ToList());

            await Dispatcher.InvokeAsync(() => { DataGrid.ItemsSource = students; });

            foreach (var item in AppData.db.Courses) CourseNames.Add(item.Name);

            (DataGrid.Columns[4] as DataGridComboBoxColumn).ItemsSource = CourseNames;
        }
    }
    
}
