using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Beeswax.Models;
using Beeswax.Service;

namespace Beeswax.ViewModel
{
    public class ShoppPageViewModel : BaseViewModel
    {
        private readonly ProductoSer _productoSer;
        private readonly IDialogService _dialogService;

        private ObservableCollection<Producto> _products;
        public ObservableCollection<Producto> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }

        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand PurchaseCommand { get; }

        public ShoppPageViewModel()
        {
            _productoSer = new ProductoSer();
            _dialogService = new DialogService();
            Products = new ObservableCollection<Producto>();

            AddCommand = new RelayCommand<Producto>(AddProduct);
            RemoveCommand = new RelayCommand<Producto>(RemoveProduct);
            PurchaseCommand = new AsyncRelayCommand(PurchaseProducts);

            LoadProducts();
        }

        private async void LoadProducts()
        {
            var products = await _productoSer.Obtener();
            foreach (var product in products)
            {
                product.Quantity = 0; // Inicializar con cantidad 0
                Products.Add(product);
            }
        }

        private void AddProduct(Producto product)
        {
            if (product.stock > product.Quantity)
            {
                product.Quantity++;
            }
            else
            {
                _dialogService.ShowMessage("Stock insuficiente", "No puedes agregar más de este producto", "OK");
            }
        }

        private void RemoveProduct(Producto product)
        {
            if (product.Quantity > 0)
            {
                product.Quantity--;
            }
        }

        private async Task PurchaseProducts()
        {
            foreach (var product in Products)
            {
                if (product.Quantity > 0)
                {
                    product.stock -= product.Quantity;
                    await _productoSer.Actualizar(product);
                    product.Quantity = 0; // Resetea la cantidad después de la compra
                }
            }

            await _dialogService.ShowMessage("Compra Exitosa", "Los productos han sido comprados exitosamente.", "OK");
        }
    }
}