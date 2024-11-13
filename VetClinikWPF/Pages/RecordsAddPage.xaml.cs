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
    /// Логика взаимодействия для RecordsAddPage.xaml
    /// </summary>
    public partial class RecordsAddPage : Page
    {
        public static ObservableCollection<Animals> animals { get; set; }
        public static ObservableCollection<Owners> owners { get; set; }
        public static DB.Records addRecords = new DB.Records();
        public RecordsAddPage()
        {
            InitializeComponent();
            animals = new ObservableCollection<Animals>(DbConnections.clinikaEntities.Animals.ToList());  
            owners = new ObservableCollection<Owners>(DbConnections.clinikaEntities.Owners.ToList());
            App.records = new ObservableCollection<Records>(DbConnections.clinikaEntities.Records.ToList());
            RecordsLv.ItemsSource = App.records;
            AnimalsCb.ItemsSource = animals;
            AnimalsCb.DisplayMemberPath = "Name";
            foreach (var owner in owners)
            {
                // Предполагаем, что у вас есть свойства FirstName, LastName и MiddleName
                string fullName = $"{owner.Surname} {owner.Name} {owner.Patronymic}";
                OwnersCb.Items.Add(new ComboBoxItem { Content = fullName, Tag = owner });
            }

        }

        private void AddRecordsBtn_Click(object sender, RoutedEventArgs e)
        {

            if(AnimalsCb.SelectedItem != null || OwnersCb.SelectedItem != null || DataRecordsDp.SelectedDate != null)
            {
                var selectedItem = OwnersCb.SelectedItem as ComboBoxItem;
                var selectedOwners = selectedItem.Tag as Owners;
                addRecords.Id_Owners = selectedOwners.Id_Owner;
                addRecords.Id_Animals = (AnimalsCb.SelectedItem as Animals).Id_Animals;
                addRecords.StartDate = DataRecordsDp.SelectedDate;
                DbConnections.clinikaEntities.Records.Add(addRecords);
                DbConnections.clinikaEntities.SaveChanges();
                App.records = new ObservableCollection<Records>(DbConnections.clinikaEntities.Records.ToList());
                RecordsLv.ItemsSource = App.records;
                MessageBox.Show("Запись успешно добавлена!");
            }
            else
            {
                MessageBox.Show("Запись не добавлена!");
            }
            




        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
