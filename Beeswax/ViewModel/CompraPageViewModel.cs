using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Beeswax.Models;
using Beeswax.ViewModel;
using System.Linq;
using System.Windows.Input;
using Beeswax.Views;

namespace Beeswax.ViewModel
{

    public class CompraPageViewModel : BaseViewModel
    {
        public ObservableCollection<CompraViewModel> Compras { get; set; }

        public ICommand EditarCompraCommand { get; }
        public ICommand EliminarCompraCommand { get; }

        public CompraPageViewModel()
        {
            Compras = new ObservableCollection<CompraViewModel>();
            EliminarCompraCommand = new Command<CompraViewModel>(OnEliminarCompra);
            LoadCompras();
        }

        public async void LoadCompras()
        {
            var compras = await Database.GetComprasAsync();
            if (compras == null || !compras.Any())
            {
                Debug.WriteLine("No purchases found.");
                return;
            }

            foreach (var compra in compras)
            {
                var detalles = await Database.GetCompraDetallesAsync(compra.Id);
                if (detalles == null || !detalles.Any())
                {
                    Debug.WriteLine($"No details found for compra.Id = {compra.Id}");
                    continue;
                }

                var compraVM = new CompraViewModel
                {
                    Id = compra.Id,
                    Fecha = compra.Fecha,
                    Total = (decimal)compra.Total,
                    Detalles = new ObservableCollection<CompraDetalleViewModel>()
                };

                foreach (var detalle in detalles)
                {
                    var producto = await Database.GetProductoAsync(detalle.ProductoId);
                    if (producto == null)
                    {
                        Debug.WriteLine($"Product not found for detalle.ProductoId = {detalle.ProductoId}");
                        continue;
                    }

                    compraVM.Detalles.Add(new CompraDetalleViewModel
                    {
                        ProductoNombre = producto.nombreProducto,
                        Cantidad = detalle.Cantidad,
                        Precio = (decimal)detalle.Precio,
                        PrecioTotal = (decimal)detalle.Cantidad * (decimal)detalle.Precio
                    });
                }

                Compras.Add(compraVM);
            }
        }

        public async void OnEliminarCompra(CompraViewModel compra)
        {
            if (compra == null)
                return;

            await Database.DeleteCompraAsync(compra.Id);
            Compras.Remove(compra);
            Debug.WriteLine($"Eliminando compra: {compra.Fecha}");
        }
    }


    public class CompraViewModel
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public ObservableCollection<CompraDetalleViewModel> Detalles { get; set; } = new ObservableCollection<CompraDetalleViewModel>();
    }

    public class CompraDetalleViewModel
    {
        public string ProductoNombre { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal PrecioTotal { get; set; }
    }

}


