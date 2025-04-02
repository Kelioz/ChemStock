using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace ChemStock
{
    public partial class CategoriesPage : Page
    {
        public CategoriesPage()
        {
            InitializeComponent();
            LoadTypes();
        }

        private void LoadTypes()
        {
            try
            {
                DB db = new DB();
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();

                SqlCommand command = new SqlCommand("SELECT * FROM Type", db.getConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);

                TypesGrid.ItemsSource = table.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки типов: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var inputDialog = new InputDialog("Добавление типа", "Введите название нового типа:");
            if (inputDialog.ShowDialog() == true)
            {
                try
                {
                    DB db = new DB();
                    SqlCommand command = new SqlCommand("INSERT INTO Type (Name) VALUES (@name)", db.getConnection());
                    command.Parameters.AddWithValue("@name", inputDialog.Answer);

                    db.openConnetion();
                    command.ExecuteNonQuery();
                    db.closeConnetion();

                    MessageBox.Show("Тип успешно добавлен", "Успех",
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadTypes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при добавлении типа: {ex.Message}", "Ошибка",
                                  MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            int id = (int)button.Tag;
            var row = ((DataRowView)button.DataContext);

            var inputDialog = new InputDialog("Редактирование типа", "Введите новое название:", row["Name"].ToString());
            if (inputDialog.ShowDialog() == true)
            {
                try
                {
                    DB db = new DB();
                    SqlCommand command = new SqlCommand("UPDATE Type SET Name = @name WHERE id = @id", db.getConnection());
                    command.Parameters.AddWithValue("@name", inputDialog.Answer);
                    command.Parameters.AddWithValue("@id", id);

                    db.openConnetion();
                    command.ExecuteNonQuery();
                    db.closeConnetion();

                    MessageBox.Show("Тип успешно обновлен", "Успех",
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadTypes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при обновлении типа: {ex.Message}", "Ошибка",
                                  MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            int id = (int)button.Tag;

            var result = MessageBox.Show("Вы уверены, что хотите удалить этот тип? Это может повлиять на связанные товары.",
                                       "Подтверждение удаления",
                                       MessageBoxButton.YesNo,
                                       MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    DB db = new DB();
                    SqlCommand command = new SqlCommand("DELETE FROM Type WHERE id = @id", db.getConnection());
                    command.Parameters.AddWithValue("@id", id);

                    db.openConnetion();
                    int rowsAffected = command.ExecuteNonQuery();
                    db.closeConnetion();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Тип успешно удален", "Успех",
                                      MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadTypes();
                    }
                }
                catch (SqlException ex) when (ex.Number == 547) // Ошибка внешнего ключа
                {
                    MessageBox.Show("Нельзя удалить тип, так как он используется в товарах", "Ошибка",
                                  MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении типа: {ex.Message}", "Ошибка",
                                  MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadTypes();
        }
    }
}