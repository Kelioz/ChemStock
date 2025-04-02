using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.Win32;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using iTextSharp.text;
using System.Windows.Forms;
namespace ChemStock
{
    public partial class ItemsPage : Page
    {
        public ItemsPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                DB db = new DB();
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();

                SqlCommand command = new SqlCommand("SELECT * FROM Item_View", db.getConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);

                // Очищаем существующие колонки
                ChemTable.Columns.Clear();

                // Добавляем колонки из Item_View
                foreach (DataColumn column in table.Columns)
                {
                    if (column.ColumnName == "id") continue; // Пропускаем колонку id

                    var dataGridColumn = new DataGridTextColumn
                    {
                        Header = column.ColumnName,
                        Binding = new System.Windows.Data.Binding(column.ColumnName)
                    };

                    // Форматирование для числовых колонок
                    if (column.DataType == typeof(decimal) || column.DataType == typeof(int))
                    {
                        ((System.Windows.Data.Binding)dataGridColumn.Binding).StringFormat = "N2";
                    }

                    ChemTable.Columns.Add(dataGridColumn);
                }

                // Добавляем колонку с действиями
                var actionColumn = new DataGridTemplateColumn
                {
                    Header = "Действия",
                    Width = new DataGridLength(1, DataGridLengthUnitType.Auto),
                    CellTemplate = (DataTemplate)FindResource("DeleteButtonTemplate")
                };
                ChemTable.Columns.Add(actionColumn);

                ChemTable.ItemsSource = table.DefaultView;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            int id = (int)button.Tag;

            var result = System.Windows.MessageBox.Show("Вы уверены, что хотите удалить этот товар?",
                                      "Подтверждение удаления",
                                      MessageBoxButton.YesNo,
                                      MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    DB db = new DB();
                    SqlCommand command = new SqlCommand("DELETE FROM Items WHERE id = @id", db.getConnection());
                    command.Parameters.AddWithValue("@id", id);

                    db.openConnetion();
                    int rowsAffected = command.ExecuteNonQuery();
                    db.closeConnetion();

                    if (rowsAffected > 0)
                    {
                        System.Windows.MessageBox.Show("Товар успешно удален", "Успех",
                                      MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadData(); // Обновляем таблицу после удаления
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка",
                                  MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            int id = (int)button.Tag;
            var row = ((DataRowView)button.DataContext);

            var editWindow = new AddItemWindow(
                id,
                row["Название"].ToString(),
                row["Тип"].ToString(),
                Convert.ToInt32(row["Количество"]),
                Convert.ToDecimal(row["Цена"])
            );

            if (editWindow.ShowDialog() == true)
            {
                LoadData(); // Обновляем данные после редактирования
            }
        }
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddItemWindow addItemWindow = new AddItemWindow();
            addItemWindow.ShowDialog();
        }

        private void ExportToPdf_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Диалог сохранения файла
                var saveDialog = new System.Windows.Forms.SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    Title = "Сохранить отчет как PDF",
                    FileName = $"Отчет_по_товарам_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
                };

                if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    // Создаем документ PDF
                    Document document = new Document(PageSize.A4.Rotate(), 10, 10, 10, 10);
                    PdfWriter.GetInstance(document, new FileStream(saveDialog.FileName, FileMode.Create));
                    document.Open();

                    // Шрифты
                    BaseFont baseFont = BaseFont.CreateFont("c:\\windows\\fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                    Font headerFont = new Font(baseFont, 16, Font.BOLD);
                    Font normalFont = new Font(baseFont, 10);
                    Font boldFont = new Font(baseFont, 10, Font.BOLD);

                    // Заголовок
                    Paragraph header = new Paragraph("Отчет по товарам на складе", headerFont);
                    header.Alignment = Element.ALIGN_CENTER;
                    header.SpacingAfter = 20;
                    document.Add(header);

                    // Дата формирования
                    Paragraph date = new Paragraph($"Дата формирования: {DateTime.Now:dd.MM.yyyy HH:mm}", normalFont);
                    date.Alignment = Element.ALIGN_RIGHT;
                    document.Add(date);

                    // Создаем таблицу
                    PdfPTable table = new PdfPTable(ChemTable.Columns.Count - 1); // -1 чтобы исключить колонку с действиями
                    table.WidthPercentage = 100;

                    // Заголовки таблицы
                    foreach (var column in ChemTable.Columns)
                    {
                        if (column.Header != null && column.Header.ToString() != "Действия")
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(column.Header.ToString(), boldFont));
                            cell.BackgroundColor = new BaseColor(240, 240, 240);
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.Padding = 5;
                            table.AddCell(cell);
                        }
                    }

                    // Данные таблицы
                    foreach (DataRowView row in ChemTable.ItemsSource)
                    {
                        foreach (var column in ChemTable.Columns)
                        {
                            if (column.Header != null && column.Header.ToString() != "Действия")
                            {
                                var content = row[column.Header.ToString()];
                                string text = content?.ToString() ?? string.Empty;

                                PdfPCell cell = new PdfPCell(new Phrase(text, normalFont));
                                cell.Padding = 5;
                                table.AddCell(cell);
                            }
                        }
                    }

                    document.Add(table);

                    // Итоговая информация
                    Paragraph footer = new Paragraph($"Всего товаров: {ChemTable.Items.Count}", boldFont);
                    footer.SpacingBefore = 20;
                    document.Add(footer);

                    document.Close();

                    System.Windows.MessageBox.Show("Отчет успешно сохранен в PDF", "Успех",
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Ошибка при экспорте в PDF: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}