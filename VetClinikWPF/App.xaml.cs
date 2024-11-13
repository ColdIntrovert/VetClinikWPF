using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using VetClinikWPF.DB;

namespace VetClinikWPF
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ObservableCollection<Users> users = new ObservableCollection<Users>();
        public static DB.Users addUsers = new DB.Users();
        public static ObservableCollection<Records> records { get; set; }
        public static int id_Us;

    }
}
