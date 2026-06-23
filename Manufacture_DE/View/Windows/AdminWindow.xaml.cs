using Manufacture_DE.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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

namespace Manufacture_DE.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();

            UsersLv.ItemsSource = App.context.Customer.ToList();
            RoleCmb.ItemsSource = App.context.UserRole.ToList();
        }

        private void AddUserBtn_Click(object sender, RoutedEventArgs e)
        {
            UsersLv.SelectedItem = null;
            SaveChangesBtn.Visibility = Visibility.Collapsed;
            NewBtn.Visibility = Visibility.Visible;
        }
        SystemUser sysus;
        private void UsersLv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.currentCus = UsersLv.SelectedItem as Customer;
            if (UsersLv.SelectedItem != null)
            {
                sysus = App.context.SystemUser.FirstOrDefault(u => u.Customer.Id == App.currentCus.Id);

                NameTb.Text = App.currentCus.Name;
                InnTb.Text = App.currentCus.INN?.Trim();
                AdressTb.Text = App.currentCus.Addres;
                PhoneTb.Text = App.currentCus.Phone;
                IsSalesmanCb.IsChecked = App.currentCus.IsSalesman;
                IsBuyerCb.IsChecked = App.currentCus.IsBuyer;

                LoginTb.Text = sysus.Login;
                PassTb.Text = sysus.Password;
                IsBlockedCb.IsChecked = sysus.IsBlocked;
                RoleCmb.SelectedItem = sysus.UserRole;
            }
            else
            {
                NameTb.Text = null;
                InnTb.Text = null;
                AdressTb.Text = null;
                PhoneTb.Text = null;
                IsSalesmanCb.IsChecked = false;
                IsBuyerCb.IsChecked = false;

                LoginTb.Text = null;
                PassTb.Text = null;
                IsBlockedCb.IsChecked = false;
                RoleCmb.SelectedItem = null;
            }
        }

        private void SaveChangesBtn_Click(object sender, RoutedEventArgs e)
        {
            App.currentCus.Name = NameTb.Text;
            App.currentCus.INN = InnTb.Text.Trim();
            App.currentCus.Addres = AdressTb.Text;
            App.currentCus.Phone = PhoneTb.Text;
            App.currentCus.IsSalesman = (bool)IsSalesmanCb.IsChecked;
            App.currentCus.IsBuyer = (bool)IsBuyerCb.IsChecked;

            sysus.Login = LoginTb.Text;
            sysus.Password = PassTb.Text;
            sysus.IsBlocked = (bool)IsBlockedCb.IsChecked;
            sysus.UserRole = RoleCmb.SelectedItem as UserRole;

            App.context.SaveChanges();
            UsersLv.ItemsSource = App.context.Customer.ToList();
        }

        private void NewBtn_Click(object sender, RoutedEventArgs e)
        {
            Customer newCustomer = new Customer()
            {
                Name = NameTb.Text,
                INN = InnTb.Text.Trim(),
                Addres = AdressTb.Text,
                Phone = PhoneTb.Text.Trim(),
                IsSalesman = (bool)IsSalesmanCb.IsChecked,
                IsBuyer = (bool)IsBuyerCb.IsChecked,
            };
            App.context.Customer.Add(newCustomer);
            App.context.SaveChanges();

            SystemUser newUser = new SystemUser()
            {
                Login = LoginTb.Text,
                Password = PassTb.Text,
                IsBlocked = (bool)IsBlockedCb.IsChecked,
                UserRole = RoleCmb.SelectedItem as UserRole,
                Customer = newCustomer
            };
            App.context.SystemUser.Add(newUser);
            App.context.SaveChanges();
            UsersLv.ItemsSource = App.context.Customer.ToList();

            NewBtn.Visibility = Visibility.Collapsed;
            SaveChangesBtn.Visibility = Visibility.Visible;

            UsersLv.SelectedItem = newCustomer;
        }
    }
}
