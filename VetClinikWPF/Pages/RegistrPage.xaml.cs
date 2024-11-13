using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    /// Логика взаимодействия для RegistrPage.xaml
    /// </summary>
    public partial class RegistrPage : Page
    {
        public static ObservableCollection<Animals> animals {  get; set; }
        public static ObservableCollection<Owners> owners {  get; set; }

        public static DB.Owners addOwners = new DB.Owners();
        public static DB.Animals addAnimals = new DB.Animals();

        public RegistrPage(Users infoUser)
        {
            InitializeComponent();
            NameUsTb.Text = infoUser.Name;
            SurnameUsTb.Text = infoUser.Surname;
            RoleUsTb.Text = infoUser.Roles.Name_Role;
            if(infoUser.Photo_Users == null)
            {
                PhotoUsImg.Source = new BitmapImage(new Uri(@"C:\Users\ramil\source\repos\VetClinikWPF\VetClinikWPF\Photos\NotPhoto.jpg"));
            }
            else
            {
                PhotoUsImg.Source = new BitmapImage(new Uri(infoUser.Photo_Users, UriKind.Absolute));
            }
            

            animals = new ObservableCollection<Animals>(DbConnections.clinikaEntities.Animals.ToList());
            AnimalsLv.ItemsSource = animals;
            owners = new ObservableCollection<Owners>(DbConnections.clinikaEntities.Owners.ToList());
            OwnersLv.ItemsSource = owners;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }

        private void AddAnimalsBtn_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(AnimalsNameTb.Text))
            {
                MessageBox.Show("Ошибка", "Заполните все поля для животных", MessageBoxButton.OK);

            }
            else
            {
                addAnimals.Name = AnimalsNameTb.Text;
                DbConnections.clinikaEntities.Animals.Add(addAnimals);
                DbConnections.clinikaEntities.SaveChanges();
                animals = new ObservableCollection<Animals>(DbConnections.clinikaEntities.Animals.ToList());
                AnimalsLv.ItemsSource = animals;
                AnimalsNameTb.Text = string.Empty;
                MessageBox.Show("Животное успешно добавлено!"); 

            }
        }

        private void AddOwnerBtn_Click(object sender, RoutedEventArgs e)
        {
            var textBoxes = new TextBox[] { NameTb, SurnameTb, PatronymicTb, PhoneTb };
            if (textBoxes.Any(tb => string.IsNullOrEmpty(tb.Text)))
            {
                MessageBox.Show("Ошибка", "Заполните все поля для владельцев", MessageBoxButton.OK);
                // Обработка ситуации, когда хотя бы одно поле пустое
            }
            else
            {
                addOwners.Name = NameTb.Text;
                addOwners.Surname = SurnameTb.Text;
                addOwners.Patronymic = PatronymicTb.Text;
                addOwners.Phone = PhoneTb.Text;
                DbConnections.clinikaEntities.Owners.Add(addOwners);
                DbConnections.clinikaEntities.SaveChanges();
                owners = new ObservableCollection<Owners>(DbConnections.clinikaEntities.Owners.ToList());
                OwnersLv.ItemsSource = owners;
                NameTb.Text = string.Empty;
                SurnameTb.Text = string.Empty;
                PatronymicTb.Text = string.Empty;
                PhoneTb.Text = string.Empty;
                MessageBox.Show("Владелец успено добавлен!");
            }
        }

        private void PhoneTb_TextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text); // Запретить ввод, если текст не соответствует паттерну
        }

        private void RecordsBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RecordsAddPage());
        }
    }
}
