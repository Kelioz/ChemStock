using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace ChemStock
{
    public partial class UsersPage : Page
    {
        public UsersPage()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            try
            {
                DB db = new DB();
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();

                SqlCommand command = new SqlCommand("SELECT * FROM Users_View", db.getConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);

                UsersGrid.ItemsSource = table.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки пользователей: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Открытие окна добавления пользователя
            var addUserWindow = new AddUserWindow();
            if (addUserWindow.ShowDialog() == true)
            {
                LoadUsers(); // Обновляем данные после добавления
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            int userId = (int)button.Tag;
            var row = ((DataRowView)button.DataContext);

            // Открытие окна редактирования пользователя
            var editUserWindow = new AddUserWindow(userId,
                                                 row["ФИО"].ToString(),
                                                 row["Телефон"].ToString(),
                                                 row["Роль"].ToString(),
                                                 row["Логин"].ToString(),
                                                 row["Пароль"].ToString());

            if (editUserWindow.ShowDialog() == true)
            {
                LoadUsers(); // Обновляем данные после редактирования
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            int userId = (int)button.Tag;

            var result = MessageBox.Show("Вы уверены, что хотите удалить этого пользователя?",
                                       "Подтверждение удаления",
                                       MessageBoxButton.YesNo,
                                       MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    DB db = new DB();
                    SqlCommand command = new SqlCommand("DELETE FROM Users WHERE id = @id", db.getConnection());
                    command.Parameters.AddWithValue("@id", userId);

                    db.openConnetion();
                    int rowsAffected = command.ExecuteNonQuery();
                    db.closeConnetion();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Пользователь успешно удален", "Успех",
                                      MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadUsers();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении пользователя: {ex.Message}", "Ошибка",
                                  MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadUsers();
        }
    }
}