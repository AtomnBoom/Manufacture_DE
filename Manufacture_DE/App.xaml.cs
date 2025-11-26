using Manufacture_DE.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Manufacture_DE
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Пользователь currentUser { get; set; }
        public static user15Entities context = new user15Entities();
    }
}
