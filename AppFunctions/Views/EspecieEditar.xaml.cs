namespace AppFunctions.Views;

using AppFunctions.Models;

public partial class EspecieEditar : ContentPage
{
	public EspecieEditar()
	{
		InitializeComponent();
	}

    private async void btnAlterar_Clicked(object sender, EventArgs e)
    {
        Especie p = new Especie();
        p.espId = int.Parse(etrCodigo.Text);
        p.espNome = etrNome.Text;

        await App.Db.Update(p);
        await DisplayAlert("Atenção", "Registro alterado!", "OK");

    }

}