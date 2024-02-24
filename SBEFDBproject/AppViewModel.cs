using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace SBEFDBproject
{
    public class AppViewModel: INotifyPropertyChanged
    {
        ModelContext _context;
        Customer _selectedCustomer;
        public ObservableCollection<Customer> Customers => _context.Customers.Local.ToObservableCollection();
        public ObservableCollection<Product> Products { get; private set; }

        RelayCommand _openNCForm;
        public RelayCommand OpenNCForm => _openNCForm ??
            (_openNCForm = new(OpenNewCustomerForm));

        RelayCommand _addNewCustomer;
        public RelayCommand AddNewCustomer => _addNewCustomer ??
            (_addNewCustomer = new(NewCustomerToDB));

        RelayCommand _editCustomer;
        public RelayCommand EditCustomer => _editCustomer ??
            (_editCustomer = new(EditCustomerToDB));

        RelayCommand _deleteCustomer;
        public RelayCommand DeleteCustomer => _deleteCustomer ??
            (_deleteCustomer = new(DeleteCustomerFromDB));

        RelayCommand _addNewOrder;
        public RelayCommand AddNewOrder => _addNewOrder ??
            (_addNewOrder = new(NewProductToDB));

        RelayCommand _showCustomerOrders;
        public RelayCommand ShowOrders => _showCustomerOrders ??
            (_showCustomerOrders = new(ShowOrdersFromDB));

        public AppViewModel()
        {
            _context = new();
            _context.Customers.Load();
            _context.Products.Load();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        private void OpenNewCustomerForm(object obj = null)
        {
            NewCustomerForm customerForm = new();
            customerForm.Show();
        }
        private void NewCustomerToDB(object obj)
        {
            if (obj == null) return;
            var result = obj?.ToString()?.Split(' ');
            if (result?.Length != 5) return;
            foreach (var r in result)
            {
                if (string.IsNullOrEmpty(r))
                {
                    MessageBox.Show("invalid input !");
                    return;
                }
            }
            
            _context.Customers.Add(new Customer
            {
                SecondName = result[0],
                Name = result[1],
                Patronymic = result[2],
                Number = result[3],
                Email = result[4]
            });

            _context.SaveChanges();
        }
        private void EditCustomerToDB(object obj)
        {
            if (obj == null) return;
            _context.Customers.Update(obj as Customer);
            _context.SaveChanges();
        }
        private void DeleteCustomerFromDB(object obj)
        {
            if (obj == null) return;
            _context.Customers.Remove(obj as Customer);
            _context.SaveChanges();
        }
        private void NewProductToDB(object obj)
        {
            if (obj == null) return;
            Random rand = new();
            _context.Products.Add(new Product
            {
                Email = (obj as Customer).Email,
                Code = rand.Next(1000, 9999).ToString(),
                Description = "some product"
            });
            _context.SaveChanges();
        }
        private void ShowOrdersFromDB(object obj)
        {
            if (obj == null) return;
            Products = [.. _context.Products.Where(p => p.Email.Equals((obj as Customer).Email))];
            OrdersForm ordersForm = new();
            ordersForm.Show();
        }
    }
    public class RelayCommand : ICommand
    {
        private Action<object> _execute;
        private Func<object, bool> _canExecute;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}

