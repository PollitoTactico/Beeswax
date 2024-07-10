using Beeswax.Service;

namespace Beeswax
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<IDialogService, DialogService>();

            MainPage = new AppShell();
        }
    }
}