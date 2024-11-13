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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VetClinikWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для ChooseUsersPage.xaml
    /// </summary>
    public partial class ChooseUsersPage : Page
    {
        public ChooseUsersPage()
        {
            InitializeComponent();
        }

        private void ButtonClick_Click(object sender, RoutedEventArgs e)
        {
            var senderBtn = sender as Button;
            NavigationService.Navigate(new RegistrationPage(senderBtn));
        }
    }
}
