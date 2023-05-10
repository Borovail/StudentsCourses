using CsvHelper;
using ExamWpf.Model;
using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
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
    internal class Convertor
    {
        public static void ExportToCsv(DataGrid dataGrid, string filePath)
        {
            using (var writer = new StreamWriter(Environment.CurrentDirectory + "Courses.csv"))

            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(AppData.db.Courses.ToList());
            }

        }





        public static void ExportToExcel(DataGrid dataGrid,string path,string fileName)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            ExcelPackage excel = new ExcelPackage();

            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");

            // Установка заголовков столбцов
            for (int i = 0; i < dataGrid.Columns.Count; i++)
            {
                workSheet.Cells[1, i + 1].Value = dataGrid.Columns[i].Header;
            }

            // Добавление данных из DataGrid в лист Excel
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

            // Сохранение рабочей книги в файл
            string filePath = Path.Combine(path, fileName)+".xlsx";

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            FileStream file = new FileStream(filePath, FileMode.CreateNew);
            excel.SaveAs(file);
            file.Close();
        }
    }
}
