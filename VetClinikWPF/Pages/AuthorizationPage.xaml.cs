using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using VetClinikWPF.DB;

namespace VetClinikWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            App.users = new ObservableCollection<Users>(DbConnections.clinikaEntities.Users.ToList());
            InitializeComponent();
        }

        private void AuthorizBtn_Click(object sender, RoutedEventArgs e)
        {
            var currentUser = App.users.FirstOrDefault(i => i.Email == EmailUserTb.Text && i.Password == PasswordUserTb.Password);

            if (currentUser != null)
            {
                if (currentUser.Roles.Name_Role == "Ветеринар")
                {
                    NavigationService.Navigate(new VeterenarPage(currentUser));
                }
                else if(currentUser.Roles.Name_Role == "Регистратор")
                {
                    NavigationService.Navigate(new RegistrPage(currentUser));
                }
                else if(currentUser.Roles.Name_Role == "Администратор")
                {
                    NavigationService.Navigate(new AdministratorPage(currentUser));
                }
            }
            else
            {
                MessageBox.Show("Введите правильно данные!", "Ошибка!", MessageBoxButton.OK);
            }

        }

        private void RegistrBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ChooseUsersPage());
        }

        private void EmailUserTb_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(EmailUserTb.Text))
            {
                EmailUserTb.Text = "Email";
            }
        }

        private void EmailUserTb_GotFocus(object sender, RoutedEventArgs e)
        {
            if (EmailUserTb.Text == "Email")
            {
                EmailUserTb.Text = string.Empty;
            }
        }

        private void PasswordUserTb_PasswordChanged(object sender, RoutedEventArgs e)
        {
        }


        private void OpenEyesBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PasswordOpenTb.Visibility = Visibility.Visible; // Показать текст
            PasswordUserTb.Visibility = Visibility.Collapsed; // Скрыть PasswordBox
            OpenEyesBtn.Visibility = Visibility.Collapsed; // Скрыть кнопку 'Открытые глаза'
            CloseEyesBtn.Visibility = Visibility.Visible; // Показать кнопку 'Закрытые глаза'
            PasswordOpenTb.Text = PasswordUserTb.Password;

        }

        private void CloseEyesBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PasswordOpenTb.Visibility = Visibility.Collapsed; // Скрыть текст
            PasswordUserTb.Visibility = Visibility.Visible; // Показать PasswordBox
            OpenEyesBtn.Visibility = Visibility.Visible; // Показать кнопку 'Открытые глаза'
            CloseEyesBtn.Visibility = Visibility.Collapsed; // Скрыть кнопку 'Закрытые глаза'
            PasswordUserTb.Password = PasswordOpenTb.Text;



        }

        private void PasswordOpenTb_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
    }
}
