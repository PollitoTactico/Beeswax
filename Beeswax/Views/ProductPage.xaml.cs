using Beeswax.Service;

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
        Console.WriteLine(productos);
        ProductCollectionView.ItemsSource = productos;

    }


}