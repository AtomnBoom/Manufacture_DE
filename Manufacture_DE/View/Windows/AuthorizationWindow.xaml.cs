using Manufacture_DE;
using Manufacture_DE.Model;
using Manufacture_DE.View.Windows;
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

namespace Autopark_DE.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        int failedEntryCount = 0;

        int inputCount = 0;
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginTb.Text) ||
                string.IsNullOrEmpty(PasswordPb.Password))
            {
                MessageBox.Show("Заполните все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                App.currentUser = App.context.Пользователь.FirstOrDefault(u => u.Login == LoginTb.Text && u.Password == PasswordPb.Password);

                if (App.currentUser != null)
                {
                    CaptchaWindow captchaWindow = new CaptchaWindow();
                    if (captchaWindow.ShowDialog() == true)
                    {
                        //Авторизация
                        if (App.currentUser.RoleId == 1)
                        {
                            AdminWindow adminWindow = new AdminWindow();
                            adminWindow.Show();
                        }
                        else
                        {
                            UserWindow userWindow = new UserWindow();
                            userWindow.Show();
                        }
                        Close();
                    }
                    else
                    {
                        //Блокировка
                        string login = App.context.Пользователь.FirstOrDefault(s => s.Login == LoginTb.Text).Login;

                        if (string.IsNullOrEmpty(login))
                        {

                        }
                        else
                        {
                            //Подсчет кол-ва неудачных попыток
                            failedEntryCount++;
                            MessageBox.Show($"Введен неверный пароль. Осталось попыток:{failedEntryCount} из 3", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);

                            if (failedEntryCount == 3)
                            {
                                MessageBox.Show("Пльзователь заблокирован!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                                failedEntryCount = 0;
                                Пользователь userToBlock = App.context.Пользователь.FirstOrDefault(s => s.Login == LoginTb.Text);
                                userToBlock.IsBlocked = true;
                                App.context.SaveChanges();
                            }
                        }
                    }
                }
            }
        }
    }
}