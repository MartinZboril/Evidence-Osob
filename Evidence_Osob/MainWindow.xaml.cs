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

namespace Evidence_Osob
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int tempSelectedIndex;
        dataService dataservice = new dataService();
        ObservableCollection<Person> persons = new ObservableCollection<Person>();
        public MainWindow()
        {
            InitializeComponent();
            DisplayPersonsAsync();
        }

        //Přidá osobu do databáze
        private async void AddButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            //Kontrola vstupů
            if (FirstName.Text.Equals("").Equals(false) && int.TryParse(FirstName.Text, out int Nothing).Equals(false) && LastName.Text.Equals("").Equals(false) && int.TryParse(LastName.Text, out int Nothing1).Equals(false) && int.TryParse(RN.Text, out int RNPart1) && int.TryParse(RN2.Text, out int RNPart2) && Sex.Text.Equals("").Equals(false))
            {
                Person item = new Person();
                item.Name = FirstName.Text;
                item.Surname = LastName.Text;
                item.RC = RNPart1 + "/" + RNPart2;
                item.DateOfBirth = DateOfBirth.Text;
                item.Gender = Sex.Text;

                await dataservice.PostPerson(item);

                ClearForm();
                Message.Text = "Osoba byla úspěšně přidána!";

                await DisplayPersonsAsync();
            }
            else
            {
                Message.Text = "Špatně vypněno!";
                await DisplayPersonsAsync();
            }
        }

        //Vybrání určitého záznamu z listview
        private void PersonListView_SelectionChangedAsync(object sender, SelectionChangedEventArgs e)
        {
            if (PersonListView.SelectedItem != null)
            {
                AddButton.Visibility = Visibility.Hidden;
                UpdateButton.Visibility = Visibility.Visible;
                DeleteButton.Visibility = Visibility.Visible;
                RN.IsEnabled = false;
                RN2.IsEnabled = false;
                var person = PersonListView.SelectedItem as Person;
                tempSelectedIndex = PersonListView.SelectedIndex;
                FirstName.Text = person.Name;
                LastName.Text = person.Surname;
                string[] RNnum = person.RC.Split(("/").ToCharArray());
                RN.Text = RNnum[0].ToString();
                RN2.Text = RNnum[1].ToString();
                Sex.Text = person.Gender;
                Message.Text = "";
            }
            else
            {
                AddButton.Visibility = Visibility.Visible;
                UpdateButton.Visibility = Visibility.Hidden;
                DeleteButton.Visibility = Visibility.Hidden;
                RN.IsEnabled = true;
                RN2.IsEnabled = true;
                ClearForm();
            }
        }

        //Změna záznamu v databázi
        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            //Kontrola vstupů
            if (FirstName.Text.Equals("").Equals(false) && int.TryParse(FirstName.Text, out int Nothing).Equals(false) && LastName.Text.Equals("").Equals(false) && int.TryParse(LastName.Text, out int Nothing1).Equals(false) && int.TryParse(RN.Text, out int RNPart1) && int.TryParse(RN2.Text, out int RNPart2) && Sex.Text.Equals("").Equals(false))
            {
                Person item = new Person();
                item.Name = FirstName.Text;
                item.Surname = LastName.Text;
                item.RC = RNPart1 + "/" + RNPart2;
                item.DateOfBirth = DateOfBirth.Text;
                item.Gender = Sex.Text;

                await dataservice.UpdatePersonsync(item);

                ClearForm();
                Message.Text = "Osoba byla úspěšně změněna!";

                await DisplayPersonsAsync();
            }
            else
            {
                Message.Text = "Špatně vypněno!";
                await DisplayPersonsAsync();
            }
        }

        //Odstranění záznamu
        private async void DeleteButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            PersonListView.SelectedIndex = tempSelectedIndex;
            var person = PersonListView.SelectedItem as Person;
            await dataservice.DeletePersonAsync(person);
            Message.Text = "Osoba byla smazána!";
            await DisplayPersonsAsync();
        }

        public async Task DisplayPersonsAsync()
        {
            persons = await dataservice.GetPersonsListAsync();
            PersonListView.ItemsSource = persons;
        }

        //Vyprázdnění formuláře po odeslání
        private void ClearForm()
        {
            FirstName.Text = " ";
            LastName.Text = " ";
            RN.Text = " ";
            RN2.Text = " ";
            DateOfBirth.Text = " ";
            Sex.Text = " ";
        }
    }
}
