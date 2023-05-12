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
using System.IO;
using System.Net.NetworkInformation;

namespace ExamWpf.Pages
{
    /// <summary>
    /// Логика взаимодействия для PathPage.xaml
    /// </summary>
    public partial class PathPage : Page
    {
        DataGrid DataDrid;
        ConvertType ConvertType;
        string path;
        string FileName;
        public PathPage(DataGrid dataGrid, ConvertType convertType)
        {
            InitializeComponent();

            DataDrid = dataGrid;

            ConvertType = convertType;
        }

        void Confirm_btn_Click(object sender, RoutedEventArgs e)
        {
            path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            FileName = "ConvertedFile";

            if (Name_textBox.Text != "" && ConvertType == ConvertType.Csv)
            {
                FileName = Name_textBox.Text;
            }
            else if (Name_textBox.Text != "" && ConvertType == ConvertType.Excel)
            {
                FileName = Name_textBox.Text;
            }

            if (Path_textBox.Text != "")
            {
                path = Path_textBox.Text;
            }

            if (ConvertType == ConvertType.Csv)
            {
                FileName += ".csv";

                if (!Convertor.ExportToCsv(DataDrid, path, FileName)) return;
            }
            if (ConvertType == ConvertType.Excel)
            {
                FileName += ".xlsx";

                if (!Convertor.ExportToExcel(DataDrid, path, FileName)) return;

            }

            MessageBox.Show("File succesfully created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            NavigationService.GoBack();

        }

        private void Back_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
