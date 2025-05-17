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

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            MenuItem selecionado = sender as MenuItem;
            Especie p = selecionado.BindingContext as Especie;

            bool confirma =
                await DisplayAlert("Atenção", "Confirma a remoção?", "Sim", "Não");

            if (confirma == true)
            {
                await App.Db.Delete(p.espId);
                await DisplayAlert("Aviso", "Registro removido!", "OK");

                await LoadingInfoEsp();
            }
        }

        private void lstespecie_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Especie p = e.SelectedItem as Especie;

            etrCodigo.Text = p.espId.ToString();
            if (p.espNome != null)
            {
                etrNome.Text = p.espNome.ToString();
            }

        }

        private void btnLimpar_Clicked(object sender, EventArgs e)
        {
            etrCodigo.Text = String.Empty;
            etrNome.Text = String.Empty;
        }

        private async void btnAlterar_Clicked(object sender, EventArgs e)
        {
            if (etrCodigo.Text == null)
            {
                await DisplayAlert("Atenção", "Campo Código obrigatório!", "OK");
            }
            else
            {
                if (etrNome.Text == null)
                {
                    await DisplayAlert("Atenção", "Campo Nome obrigatório!", "OK");
                }
                else
                {
                    Especie p = new Especie();
                    p.espId = int.Parse(etrCodigo.Text);
                    p.espNome = etrNome.Text;

                    await App.Db.Update(p);
                    await DisplayAlert("Atenção", "Registro alterado!", "OK");

                    await LoadingInfoEsp();
                }
            }

        }

        private void MenuItem_Clicked_1(object sender, EventArgs e)
        {
            MenuItem selecionado = sender as MenuItem;
            Especie p = selecionado.BindingContext as Especie;

            Navigation.PushAsync(new Views.EspecieEditar { BindingContext = p });
        }
    }

}