using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExamWpf.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }


        private void Students_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.Studens());
        }

        private void Courses_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.Courses());
        }

    }
}
