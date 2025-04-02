using System.Windows;

namespace ChemStock
{
    public partial class InputDialog : Window
    {
        public string newTitle { get; set; }
        public string Message { get; set; }
        public string Answer { get; set; }

        public InputDialog(string title, string message, string defaultAnswer = "")
        {
            InitializeComponent();
            newTitle = title;
            Message = message;
            Answer = defaultAnswer;
            DataContext = this;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}