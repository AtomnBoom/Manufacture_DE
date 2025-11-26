using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Manufacture_DE.AppData
{
    internal class Captcha
    {
        private const string key = "0123";
        List<CaptchaFragment> captchaFragments;
        public List<CaptchaFragment> CreateFragments(string[] imagesPath)
        {
            captchaFragments = new List<CaptchaFragment>();

            for (int i = 0; i < imagesPath.Length; i++)
            {
                CaptchaFragment fragment = new CaptchaFragment()
                {
                    ImageSource = new BitmapImage(new Uri(imagesPath[i])),
                    Index = i
                };

                captchaFragments.Add(fragment);
            }

            return captchaFragments;
        }

        public bool IsCorrect(ItemCollection items)
        {
            string result = string.Empty;

            foreach (var fragment in items)
            {
                result += (fragment as CaptchaFragment).Index.ToString();
            }

            if (result == key)
            {
                return true;
            }

            return false;
        }
    }
}