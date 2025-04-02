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

namespace ChemStock
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int role {  set; get; }
        public MainWindow(int role)
        {
            InitializeComponent();
            ItemsPage itemsPage = new ItemsPage();
            MainFrame.Navigate(itemsPage);
            this.role = role;
        }

        private void MainButton_Click(object sender, RoutedEventArgs e)
        {
            ItemsPage itemsPage = new ItemsPage();
            MainFrame.Navigate(itemsPage);
        }

        private void CatButton_Click(object sender, RoutedEventArgs e)
        {
            if (role == 1)
            {
                CategoriesPage categoriesPage = new CategoriesPage();
                MainFrame.Navigate(categoriesPage);
            }
            else
            {
                MessageBox.Show("У вас нет прав");
            }

        }

        private void EmployerButton_Click(object sender, RoutedEventArgs e)
        {

            if (role == 1)
            {
                UsersPage usersPage = new UsersPage();
                MainFrame.Navigate(usersPage);
            }
            else
            {
                MessageBox.Show("У вас нет прав");
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
