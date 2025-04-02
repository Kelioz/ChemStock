using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace ChemStock
{
    public partial class AddItemWindow : Window
    {
        private int? _itemId = null;
        private string _windowTitle = "Добавить товар";
        private string _actionButtonText = "Добавить";

        public AddItemWindow()
        {
            InitializeComponent();
            Title = _windowTitle;
            LoadTypes();
        }

        public AddItemWindow(int itemId, string name, string typeName, int amount, decimal price)
            : this()
        {
            _itemId = itemId;
            _windowTitle = "Редактировать товар";
            _actionButtonText = "Сохранить";
            Title = _windowTitle;

            NameTextBox.Text = name;
            AmountTextBox.Text = amount.ToString();
            PriceTextBox.Text = price.ToString();

            // Установим выбранный тип после загрузки данных
            Loaded += (s, e) =>
            {
                foreach (DataRowView item in TypeComboBox.Items)
                {
                    if (item["Name"].ToString() == typeName)
                    {
                        TypeComboBox.SelectedItem = item;
                        break;
                    }
                }
            };
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

                TypeComboBox.ItemsSource = table.DefaultView;
                if (table.Rows.Count > 0 && !_itemId.HasValue)
                {
                    TypeComboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки типов: {ex.Message}", "Ошибка",
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
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                TypeComboBox.SelectedItem == null ||
                string.IsNullOrWhiteSpace(AmountTextBox.Text) ||
                string.IsNullOrWhiteSpace(PriceTextBox.Text))
            {
                MessageBox.Show("Заполните все поля!", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                DB db = new DB();
                SqlCommand command;

                if (_itemId.HasValue)
                {
                    // Редактирование существующего товара
                    command = new SqlCommand(
                        "UPDATE Items SET Name = @name, Type = @type, Amount = @amount, price = @price WHERE id = @id",
                        db.getConnection());
                    command.Parameters.AddWithValue("@id", _itemId.Value);
                }
                else
                {
                    // Добавление нового товара
                    command = new SqlCommand(
                        "INSERT INTO Items (Name, Type, Amount, price) VALUES (@name, @type, @amount, @price)",
                        db.getConnection());
                }

                command.Parameters.AddWithValue("@name", NameTextBox.Text);
                command.Parameters.AddWithValue("@type", (TypeComboBox.SelectedItem as DataRowView)["id"]);
                command.Parameters.AddWithValue("@amount", Convert.ToInt32(AmountTextBox.Text));
                command.Parameters.AddWithValue("@price", Convert.ToDecimal(PriceTextBox.Text));

                db.openConnetion();
                command.ExecuteNonQuery();
                db.closeConnetion();

                MessageBox.Show("Товар успешно сохранен!", "Успех",
                              MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении товара: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}