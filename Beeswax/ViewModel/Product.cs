using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Beeswax.ViewModel
{
    class Product : ObservableObject
    {

        private int _quantity;

        public string nombreProducto { get; set; }
        public decimal precio { get; set; }
        public int stock { get; set; }
        public string imagen { get; set; }

        public int Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value);
        }
    }
}