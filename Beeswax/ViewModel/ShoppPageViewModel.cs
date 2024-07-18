using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Beeswax.Models;
using Beeswax.Service;
using System.Linq;
using Newtonsoft.Json;

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
                product.Quantity = 0;
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
            float total = 0;
            var detallesCompra = new List<CompraDetalle>();

            foreach (var product in Products)
            {
                if (product.Quantity > 0)
                {
                    total += product.precio * product.Quantity;

                    detallesCompra.Add(new CompraDetalle
                    {
                        Id = 0,
                        ProductoId = product.Id,
                        Cantidad = product.Quantity,
                        Precio = product.precio
                    });

                    product.stock -= product.Quantity;
                    await _productoSer.Actualizar(product);
                    product.Quantity = 0;
                }
            }

            var compra = new Compra
            {
                Id = 0,
                Fecha = DateTime.Now,
                Total = total
            };

            await Database.SaveCompraAsync(compra);

            foreach (var detalle in detallesCompra)
            {
                detalle.CompraId = compra.Id;
                await Database.SaveCompraDetalleAsync(detalle);
            }

            var mensajeCompra = $"Compra Exitosa\nTotal: ${total:F2}\n\nDetalles de la compra:\n";
            foreach (var detalle in detallesCompra)
            {
                var producto = Products.FirstOrDefault(p => p.Id == detalle.ProductoId);
                if (producto != null)
                {
                    var precioTotalProducto = detalle.Precio * detalle.Cantidad;
                    mensajeCompra += $"Nombre Producto: {producto.nombreProducto}\n" +
                                     $"Cantidad Comprada: {detalle.Cantidad}\n" +
                                     $"Cantidad que queda en stock: {producto.stock}\n" +
                                     $"Precio Total del Producto: ${precioTotalProducto:F2}\n\n";
                }
            }

            mensajeCompra += $"Precio Final de la Compra: ${total:F2}";

            await _dialogService.ShowMessage("Compra Exitosa", mensajeCompra, "OK");
        }

    }
}

