using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.Windows.Shapes;

namespace ChemStock
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string loginUser = NameTextBox.Text;
            string password = PasswordBox.Password;

            DB dB = new DB();
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand(@"SELECT Role,Login, Passwored From Users Where login = @uL AND passwored = @uP", dB.getConnection());
            command.Parameters.Add("@uL", SqlDbType.VarChar).Value = loginUser;
            command.Parameters.Add("@uP", SqlDbType.VarChar).Value = password;
            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                string group = table.Rows[0]["role"].ToString();
                if (group == "1")
                {
                    MainWindow mainWindowAdmin = new MainWindow(Convert.ToInt32(group));
                    mainWindowAdmin.Show();
                    this.Close();
                }
                else if (group == "2")
                {
                    MainWindow mainWindowManager = new MainWindow(Convert.ToInt32(group));
                    mainWindowManager.Show();
                    this.Close();

                }
                else
                {
                    MessageBox.Show("У вас нет доступа");
                }

            }
        }
    }
}
