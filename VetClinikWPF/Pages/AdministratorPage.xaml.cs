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
using VetClinikWPF.DB;
using System.Collections.ObjectModel;

namespace VetClinikWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdministratorPage.xaml
    /// </summary>
    public partial class AdministratorPage : Page
    {
        public static ObservableCollection<Animals> animals { get; set; }
        public static ObservableCollection<Owners> owners { get; set; }

        public AdministratorPage(Users infoUser)
        {
            InitializeComponent();

            App.records = new ObservableCollection<Records>(DbConnections.clinikaEntities.Records.ToList());
            RecordsLv.ItemsSource = App.records;

            animals = new ObservableCollection<Animals>(DbConnections.clinikaEntities.Animals.ToList());
            
            owners = new ObservableCollection<Owners>(DbConnections.clinikaEntities.Owners.ToList());

            animals.Insert(0, new Animals { Name = "Пусто" });
            AnimalsCb.ItemsSource = null; // Обнуляем ItemsSource
            AnimalsCb.ItemsSource = animals; // Устанавливаем новый ItemsSource
            AnimalsCb.DisplayMemberPath = "Name";

            foreach (var owner in owners)
            {
                // Предполагаем, что у вас есть свойства FirstName, LastName и MiddleName
                string fullName = $"{owner.Surname} {owner.Name} {owner.Patronymic}";
                OwnersCb.Items.Add(new ComboBoxItem { Content = fullName, Tag = owner });
                
            }
            // Добавление элемента "Пусто" для владельцев
            OwnersCb.Items.Insert(0, new ComboBoxItem { Content = "Пусто", Tag = null });
        }



        private void UpdateRecords()
        {
            // Начнем с полной выборки всех записей
            var recordsQuery = DbConnections.clinikaEntities.Records.AsQueryable();

            // Фильтруем по выбранному животному
            if (AnimalsCb.SelectedItem is Animals selectedAnimal && selectedAnimal.Name != "Пусто")
            {
                recordsQuery = recordsQuery.Where(x => x.Id_Animals == selectedAnimal.Id_Animals);
            }

            // Фильтруем по выбранному владельцу
            if (OwnersCb.SelectedItem is ComboBoxItem selectedOwnerItem)
            {
                var selectedOwners = selectedOwnerItem.Tag as Owners;
                if (selectedOwners != null && selectedOwnerItem.Content.ToString() != "Пусто")
                {
                    recordsQuery = recordsQuery.Where(x => x.Id_Owners == selectedOwners.Id_Owner);
                }
            }

            // Фильтруем по выбранной дате
            if (DataRecordsDb.SelectedDate.HasValue)
            {
                recordsQuery = recordsQuery.Where(x => x.StartDate == DataRecordsDb.SelectedDate);
            }

            // Применяем сортировку
            if (SortCb.SelectedItem is ComboBoxItem selectedSortItem)
            {
                if (selectedSortItem.Content.ToString() == "По возрастанию")
                {
                    recordsQuery = recordsQuery.OrderBy(x => x.Owners.Name);
                }
                else if (selectedSortItem.Content.ToString() == "По убыванию")
                {
                    recordsQuery = recordsQuery.OrderByDescending(x => x.Owners.Name);
                }
            }

            // Обновляем записи и привязываем к ListView
            App.records = new ObservableCollection<Records>(recordsQuery.ToList());
            RecordsLv.ItemsSource = App.records;
        }

        private void AnimalsCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateRecords();
        }

        private void OwnersCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateRecords();
        }

        private void SortCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateRecords();
        }

        private void DataRecordsDb_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateRecords();
        }

        private void NotSortBtn_Click(object sender, RoutedEventArgs e)
        {
            AnimalsCb.SelectedIndex = -1; // Сбрасываем выбор
            OwnersCb.SelectedIndex = -1; // Сбрасываем выбор
            SortCb.SelectedIndex = -1; // Сбрасываем выбор
            DataRecordsDb.SelectedDate = null; // Сбрасываем выбор даты
            UpdateRecords(); // Обновляем записи
        }

    }
}
