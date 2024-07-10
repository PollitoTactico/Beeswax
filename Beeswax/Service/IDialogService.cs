using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beeswax.Service
{
    public interface IDialogService
    {
        Task ShowMessage(string title, string message, string buttonText);
    }

    public class ConsoleDialogService : IDialogService
    {
        public Task ShowMessage(string title, string message, string buttonText)
        {
            Console.WriteLine($"{title}: {message}");
            return Task.CompletedTask;
        }
    }
}

