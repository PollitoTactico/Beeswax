using SQLite;

namespace Beeswax.Models
{
    [Table("producto")]
    public class Producto
    {
        public Producto()
        {
            Quantity = 0;
            precio = 0;
            stock = 0;

        }
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string nombreProducto { get; set; }
        public string descripcion { get; set; }
        public float precio { get; set; }
        public string categoria { get; set; }
        public int stock { get; set; }
        public string imagen { get; set; }

        [Ignore]
        public int Quantity { get; set; }
    }
}
