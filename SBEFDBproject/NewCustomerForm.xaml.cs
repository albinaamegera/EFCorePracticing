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
using System.Windows.Shapes;

namespace SBEFDBproject
{
    /// <summary>
    /// Логика взаимодействия для NewCustomerForm.xaml
    /// </summary>
    public partial class NewCustomerForm : Window
    {
        public NewCustomerForm()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Model.AddNewCustomer.Execute($"{secondname.Text} {name.Text} {patronymic.Text} {number.Text} {email.Text}");
            
            Close();
        }
    }
}
