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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private void Convert_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationGrid.Visibility = Visibility.Hidden;
            ConvertGrid.Visibility = Visibility.Visible;
        }

        private void Students_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Studens());
        }

        private void Courses_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Courses());
        }

        private void Back_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationGrid.Visibility = Visibility.Visible;
            ConvertGrid.Visibility = Visibility.Hidden;
        }
    }
}
