using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Beeswax.Service
{
    public class DialogService : IDialogService
    {
        public async Task ShowMessage(string title, string message, string cancelButton)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancelButton);
        }
    }
}
