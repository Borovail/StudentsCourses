using CsvHelper;
using ExamWpf.Model;
using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ExamWpf
{
    public enum ConvertType
    {
       Csv, Excel
    }
    internal class Convertor
    {

        public static bool ExportToCsv(DataGrid dataGrid, string pathName, string fileName)
        {

            string FilePath=Path.Combine(pathName, fileName);

            if (File.Exists(FilePath))
            {
                if (MessageBox.Show("File already exists, you want to recreate it?", "File path: " + pathName + fileName, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    File.Delete(FilePath);
                }
                else return false;
            }

            using (StreamWriter writer = new StreamWriter(FilePath))
            {
                foreach (var column in dataGrid.Columns)
                {
                    writer.Write(column.Header);

                    if (column != dataGrid.Columns.Last())
                    {
                        writer.Write("\t");
                    }
                }

                writer.WriteLine();

                foreach (var item in dataGrid.Items)
                {
                    var properties = item.GetType().GetProperties();

                    for (int i = 0; i < dataGrid.Columns.Count; i++)
                    {
                        var value = properties.FirstOrDefault(p => p.Name == dataGrid.Columns[i].SortMemberPath)?.GetValue(item);

                        if (value != null)
                        {
                            writer.Write(value.ToString());
                        }

                        if (dataGrid.Columns[i] != dataGrid.Columns.Last())
                        {
                            writer.Write("\t");
                        }
                    }
                        
                    

                    writer.WriteLine();
                }
            }
                return true;

        }

        public static bool ExportToExcel(DataGrid dataGrid,string path,string fileName)
        {
            string filePath = Path.Combine(path, fileName);

            if (File.Exists(filePath))
            {
                if (MessageBox.Show("File already exists, you want to recreate it?", "File path: " + path + fileName, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    File.Delete(filePath);
                }
                else return false;
            }

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            ExcelPackage excel = new ExcelPackage();

            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");

            for (int i = 0; i < dataGrid.Columns.Count; i++)
            {
                workSheet.Cells[1, i + 1].Value = dataGrid.Columns[i].Header;
            }

            for (int i = 0; i < dataGrid.Items.Count; i++)
            {
                var dataGridRow = dataGrid.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;

                if (dataGridRow != null)
                {
                    var rowData = dataGrid.Items[i];
                    for (int j = 0; j < dataGrid.Columns.Count; j++)
                    {
                        var bindingPath = (dataGrid.Columns[j].ClipboardContentBinding as Binding)?.Path.Path;
                        if (!string.IsNullOrEmpty(bindingPath))
                        {
                            var cellValue = rowData.GetType().GetProperty(bindingPath)?.GetValue(rowData)?.ToString();
                            workSheet.Cells[i + 2, j + 1].Value = cellValue;
                        }
                    }
                }
            }

           
           
                using (FileStream file = new FileStream(filePath, FileMode.CreateNew))
                {
                    excel.SaveAs(file);
                }
            
            return true;
           
        }
    }
}
