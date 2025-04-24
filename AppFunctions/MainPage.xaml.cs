using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppFunctions.Models;

namespace AppFunctions
{
    public partial class MainPage : ContentPage
    {

        ObservableCollection<Especie> listEsp = new ObservableCollection<Especie>();
        // List<Especie> listEsp = new List<Especie>();

        public MainPage()
        {
            InitializeComponent();
            viewEspecie.ItemsSource = listEsp;
        }

        protected override async void OnAppearing()
        {
            await LoadingInfoEsp();
        }

        private async Task LoadingInfoEsp()
        {
            List<Especie> temp = await App.Db.GetAll();
            listEsp.Clear();

            foreach (Especie especie in temp)
            {
                listEsp.Add(especie);
            }

        }

        private void btnInsertClicked(object sender, EventArgs e)
        {
            Especie esp = new Especie();
            esp.espNome = etrNome.Text;

            App.Db.Insert(esp);
            DisplayAlert("Sucesso!", "Registro inserido com êxito", "Ok");

            OnAppearing();

        }

        private async void searchBarChanged(object sender, TextChangedEventArgs e)
        {
            string query = e.NewTextValue;

            listEsp.Clear();
            List<Especie> temp = await App.Db.Search(query);

            foreach(Especie especie in temp)
            {
                listEsp.Add(especie);
            }

        }
    }

}
