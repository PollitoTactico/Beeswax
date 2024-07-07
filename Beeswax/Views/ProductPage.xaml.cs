using Beeswax.Models;
using Beeswax.Service;
using System.Runtime.CompilerServices;

namespace Beeswax.Views;

public partial class ProductPage : ContentPage
{
    
        private readonly ProductoSer _productoService;

        public ProductPage()
        {
            InitializeComponent();
            _productoService = new ProductoSer();
            LoadProducts();
        }

        private async void LoadProducts()
        {
            var productos = await _productoService.Obtener();
            ProductCollectionView.ItemsSource = productos;
        }
    

}