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
using System.Windows.Shapes;
using VetClinikWPF.DB;

namespace VetClinikWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для RecordsWindow.xaml
    /// </summary>
    public partial class RecordsWindow : Window
    {
        public event Action RecordsUpdated;

        public static DB.Records editRecords = new DB.Records();    
        public RecordsWindow(Records recordsInfo)
        {
            InitializeComponent();
            editRecords  = recordsInfo;
            NameOwnerTb.Text = recordsInfo.Owners.Name;
            NameAnimalsTb.Text = recordsInfo.Animals.Name;
           DateRecordsTb.Text = recordsInfo.StartDate.ToString();
            DiagTb.Text = recordsInfo.Desc_Diagnoz;
            DeasTb.Text = recordsInfo.Desc_Disease;
            TreatTb.Text = recordsInfo.Desc_Treatment;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            editRecords.Desc_Diagnoz = DiagTb.Text;
            editRecords.Desc_Disease = DeasTb.Text;
            editRecords.Desc_Treatment = TreatTb.Text;
            DbConnections.clinikaEntities.SaveChanges();

            

            // Вызов события обновления
            RecordsUpdated?.Invoke();
            
            
            MessageBox.Show("Сохранение прошло успешно");
        }

        

    }
}
