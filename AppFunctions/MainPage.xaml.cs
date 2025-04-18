using AppFunctions.Models;

namespace AppFunctions
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private void btnInsertClicked(object sender, EventArgs e)
        {
            Especie esp = new Especie();
            esp.Nome = etrNome.Text;

            App.Db.Insert(esp);
            DisplayAlert("Sucesso!", "Registro inserido com êxito", "Ok");

        }
    }

}
