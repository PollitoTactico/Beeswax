namespace Beeswax.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string usuario = UsuarioEntry.Text;
        string contrasena = ContrasenaEntry.Text;

        if (usuario == "admin" && contrasena == "pollito")
        {
            // Navegar a la p�gina principal despu�s de iniciar sesi�n correctamente
            await Navigation.PushAsync(new ProductPage());
        }
        else
        {
            await DisplayAlert("Error", "Usuario o contrase�a incorrectos", "OK");
        }
    }
}