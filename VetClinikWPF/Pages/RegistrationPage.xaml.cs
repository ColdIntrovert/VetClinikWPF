using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        string choose;
        string wallPhoto;
        public RegistrationPage(Button contextChoose)
        {
            InitializeComponent();
            choose = contextChoose.Content.ToString();
            if(ProfileImg.Source == null)
            {
                ProfileImg.Source = new BitmapImage(new Uri(@"C:\Users\ramil\source\repos\VetClinikWPF\VetClinikWPF\Photos\NotPhoto.jpg"));
            }

        }

        private void RegistrBtn_Click(object sender, RoutedEventArgs e)
        {
            var textBoxes = new TextBox[] { NameTb, SurnameTb, PatronymicTb, PhoneTb, PasswordTb, EmailTb };
            if (textBoxes.Any(tb => string.IsNullOrEmpty(tb.Text)))
            {
                MessageBox.Show("Ошибка", "Заполните все поля и выберите фото профиля", MessageBoxButton.OK);
                // Обработка ситуации, когда хотя бы одно поле пустое
            }
            else
            {
                var role = DbConnections.clinikaEntities.Roles.FirstOrDefault(r => r.Name_Role == choose);
                App.addUsers.Name = NameTb.Text;
                App.addUsers.Surname = SurnameTb.Text;
                App.addUsers.Patronymic = PatronymicTb.Text;
                App.addUsers.Phone = PhoneTb.Text;
                App.addUsers.Email = EmailTb.Text;
                App.addUsers.Password = PasswordTb.Text;
                App.addUsers.Id_Role = role.Code_Role;
                
                if (wallPhoto != null)
                {
                    
                    App.addUsers.Photo_Users = wallPhoto;
                   
                    
                }

                DbConnections.clinikaEntities.Users.Add(App.addUsers);
                DbConnections.clinikaEntities.SaveChanges();
                MessageBox.Show("Успешно додбавлен новый сотрудник!");
            }
        }

        private void PhoneTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text); // Запретить ввод, если текст не соответствует паттерну
        }

        private void ProfileImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string selectedPath = OpenImageDialog();

            // Обновляем свойство MainImagePath для editService
            if (selectedPath != null)
            {
                SetServiceImage(selectedPath);
                wallPhoto = selectedPath;
            }
        }

        private string OpenImageDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Image Files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg",
                Title = "Выберите изображение"
            };

            return openFileDialog.ShowDialog() == true ? openFileDialog.FileName : null;
        }

        private void SetServiceImage(string photoPath)
        {
            if (File.Exists(photoPath))
            {
                ProfileImg.Source = new BitmapImage(new Uri(photoPath, UriKind.Absolute));
            }
            else
            {
                ProfileImg.Source = null; // Заглушка
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }
    }
}
