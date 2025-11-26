using Manufacture_DE.AppData;
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
    /// Логика взаимодействия для CaptchaWindow.xaml
    /// </summary>
    public partial class CaptchaWindow : Window
    {
        Captcha captcha;
        string[] imagesPath;
        public CaptchaWindow()
        {
            InitializeComponent();

            captcha = new Captcha();

            imagesPath = new string[]
            {
                @"C:\Users\user15\source\repos\Manufacture_DE\Manufacture_DE\Resources\Images\Captcha\1.png",
                @"C:\Users\user15\source\repos\Manufacture_DE\Manufacture_DE\Resources\Images\Captcha\2.png",
                @"C:\Users\user15\source\repos\Manufacture_DE\Manufacture_DE\Resources\Images\Captcha\3.png",
                @"C:\Users\user15\source\repos\Manufacture_DE\Manufacture_DE\Resources\Images\Captcha\4.png",
            };


            CaptchaFragmentsLb.ItemsSource = captcha.CreateFragments(imagesPath);
        }
        private void FragmentsLb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CaptchaFragment captchaFragment = CaptchaFragmentsLb.SelectedItem as CaptchaFragment;

            CaptchaLb.Items.Add(captchaFragment);

            if (CaptchaLb.Items.Count >= 4)
            {
                if (captcha.IsCorrect(CaptchaLb.Items) == true)
                {
                    MessageBox.Show("Вы возможно робот, но я вас пропущу");
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Вы робот!");
                    DialogResult = true;
                }
            }
        }

        private void CaptchaLb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CaptchaFragment captchaFragment = CaptchaLb.SelectedItem as CaptchaFragment;

            CaptchaLb.Items.Remove(captchaFragment);
        }
    }
}
