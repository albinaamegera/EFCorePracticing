using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SBEFDBproject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static AppViewModel Model;
        public MainWindow()
        {
            InitializeComponent();

            Model = new AppViewModel();
            DataContext = Model;
            //newItem.CommandParameter = gridView.SelectedItem;
            //editItem.CommandParameter = gridView.SelectedValue;
            //deleteItem.CommandParameter = gridView.SelectedItem;
            //ordersItem.CommandParameter = gridView.SelectedItem;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Model.EditCustomer.Execute(gridView.SelectedItem);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Model.DeleteCustomer.Execute(gridView.SelectedItem);
        }

        private void NewOrder_Click(object sender, RoutedEventArgs e)
        {
            Model.AddNewOrder.Execute(gridView.SelectedItem);
        }

        private void ShowOrders_Click(object sender, RoutedEventArgs e)
        {
            Model.ShowOrders.Execute(gridView.SelectedItem);
        }
    }
}