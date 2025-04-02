using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace ChemStock
{
    public partial class AddUserWindow : Window
    {
        private int? _userId = null;
        private string _windowTitle = "Добавление пользователя";

        public AddUserWindow()
        {
            InitializeComponent();
            Title = _windowTitle;
            LoadRoles();
        }

        public AddUserWindow(int userId, string fio, string phone, string role, string login, string password)
            : this()
        {
            _userId = userId;
            _windowTitle = "Редактирование пользователя";
            Title = _windowTitle;

            FioTextBox.Text = fio;
            PhoneTextBox.Text = phone;
            LoginTextBox.Text = login;
            // Пароль не подгружаем из соображений безопасности
        }

        private void LoadRoles()
        {
            try
            {
                DB db = new DB();
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();

                SqlCommand command = new SqlCommand("SELECT * FROM Roles", db.getConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);

                RoleComboBox.ItemsSource = table.DefaultView;
                RoleComboBox.SelectedValuePath = "Name";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки ролей: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c))
                {
                    e.Handled = true;
                    break;
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FioTextBox.Text) ||
                string.IsNullOrWhiteSpace(PhoneTextBox.Text) ||
                RoleComboBox.SelectedItem == null ||
                string.IsNullOrWhiteSpace(LoginTextBox.Text) ||
                PasswordBox.SecurePassword.Length == 0)
            {
                MessageBox.Show("Заполните все поля!", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                DB db = new DB();
                SqlCommand command;

                if (_userId.HasValue)
                {
                    // Редактирование существующего пользователя
                    command = new SqlCommand(
                        "UPDATE Users SET FIO = @fio, Number = @phone, Role = (SELECT id FROM Roles WHERE Name = @role), " +
                        "Login = @login, Passwored = @password WHERE id = @id",
                        db.getConnection());
                    command.Parameters.AddWithValue("@id", _userId.Value);
                }
                else
                {
                    // Добавление нового пользователя
                    command = new SqlCommand(
                        "INSERT INTO Users (FIO, Number, Role, Login, Passwored) " +
                        "VALUES (@fio, @phone, (SELECT id FROM Roles WHERE Name = @role), @login, @password)",
                        db.getConnection());
                }

                command.Parameters.AddWithValue("@fio", FioTextBox.Text);
                command.Parameters.AddWithValue("@phone", PhoneTextBox.Text);
                command.Parameters.AddWithValue("@role", RoleComboBox.SelectedValue);
                command.Parameters.AddWithValue("@login", LoginTextBox.Text);

                // Преобразуем SecureString в обычную строку (в реальном приложении нужно хэшировать пароль!)
                string password = new System.Net.NetworkCredential(string.Empty, PasswordBox.SecurePassword).Password;
                command.Parameters.AddWithValue("@password", password);

                db.openConnetion();
                command.ExecuteNonQuery();
                db.closeConnetion();

                MessageBox.Show("Пользователь успешно сохранен", "Успех",
                              MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении пользователя: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}