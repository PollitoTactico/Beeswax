namespace Beeswax.Views
{
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
                Application.Current.MainPage = new InicioShell(); 
            }
            else
            {
                await DisplayAlert("Error", "Usuario o contraseña incorrectos", "OK");
            }
        }
    }
}
