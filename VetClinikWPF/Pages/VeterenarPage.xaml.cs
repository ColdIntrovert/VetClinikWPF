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
    /// Логика взаимодействия для VeterenarPage.xaml
    /// </summary>
    public partial class VeterenarPage : Page
    {
        
        public VeterenarPage( Users infoUser)
        {
            InitializeComponent();
            App.id_Us = infoUser.Id_Users;
            NameUsTb.Text = infoUser.Name;
            SurnameUsTb.Text = infoUser.Surname;
            RoleUsTb.Text = infoUser.Roles.Name_Role;
            PhotoUsImg.Source = new BitmapImage(new Uri(infoUser.Photo_Users, UriKind.Absolute));
            var uniqueRecords = DbConnections.clinikaEntities.Records.Where(x => x.Id_Veterenar == App.id_Us).ToList();
            App.records = new ObservableCollection<Records>(uniqueRecords);
            RecordsLv.ItemsSource = App.records;
        }

        private void ClientsServiceLv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
                

                RecordsWindow recordsWindow = new RecordsWindow(RecordsLv.SelectedItem as Records);
            recordsWindow.RecordsUpdated += OnRecordsUpdated; // Подписка на событие
            recordsWindow.Show();
        }





        private void OnRecordsUpdated()
        {
            // Логика для обновления вашего ListView
            UpdateListView();
        }/* начало обновления данных */

        private void UpdateListView()
        {
            // Перезагрузите данные в вашем ListView (предполагается, что у вас есть метод для этого)
            LoadRecordsData();
        }

        private void LoadRecordsData()
        {
            // Пример получения обновленных данных из базы данных
            var records = DbConnections.clinikaEntities.Records.ToList(); // или другой метод получения данных
            RecordsLv.ItemsSource = records; // Обновляем источник данных ListView
        }// Конец обновления данных

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }
    }
}
