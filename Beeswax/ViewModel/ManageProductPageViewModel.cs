using Beeswax.Models;
using Beeswax.Service;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Beeswax.ViewModel
{
    public class ManageProductPageViewModel : BaseViewModel
    {
        private readonly ProductoSer _productoSer;
        private readonly IDialogService _dialogService;

        private ObservableCollection<Producto> _products;
        public ObservableCollection<Producto> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }

        private Producto _selectedProduct;
        public Producto SelectedProduct
        {
            get => _selectedProduct;
            set => SetProperty(ref _selectedProduct, value);
        }

        private bool _isEditing;
        public bool IsEditing
        {
            get => _isEditing;
            set => SetProperty(ref _isEditing, value);
        }

        public ICommand BuscarCommand { get; }
        public ICommand NuevoProductoCommand { get; }
        public ICommand GuardarCommand { get; }
        public ICommand EliminarCommand { get; }

        public ManageProductPageViewModel()
        {
            _productoSer = new ProductoSer();
            _dialogService = new DialogService(); // Utiliza DialogService para mostrar mensajes de alerta

            Products = new ObservableCollection<Producto>();

            BuscarCommand = new AsyncRelayCommand<string>(BuscarProducto);
            NuevoProductoCommand = new RelayCommand(NuevoProducto);
            GuardarCommand = new AsyncRelayCommand(GuardarProducto);
            EliminarCommand = new AsyncRelayCommand(EliminarProducto);

            LoadProducts();
        }

        private async void LoadProducts()
        {
            var products = await _productoSer.Obtener();
            Products.Clear();
            foreach (var product in products)
            {
                Products.Add(product);
            }
        }

        private async Task BuscarProducto(string nombre)
        {
            var products = await _productoSer.Obtener();
            Products.Clear();
            foreach (var product in products.Where(p => p.nombreProducto.Contains(nombre)))
            {
                Products.Add(product);
            }
        }

        private void NuevoProducto()
        {
            SelectedProduct = new Producto();
            IsEditing = true;
        }

        private async Task GuardarProducto()
        {
            if (SelectedProduct.Id == 0)
            {
                await _productoSer.Crear(SelectedProduct);
            }
            else
            {
                await _productoSer.Actualizar(SelectedProduct);
            }

            IsEditing = false;
            LoadProducts();
        }

        private async Task EliminarProducto()
        {
            if (SelectedProduct != null)
            {

                await _dialogService.ShowMessage("Confirmación", "¿Estás seguro que deseas eliminar este producto?", "Sí");


                await _productoSer.Eliminar(SelectedProduct.Id);
                Products.Remove(SelectedProduct);
                SelectedProduct = null;
                IsEditing = false;
                LoadProducts();
            }
        }
    }
}

