using SQLite;

namespace Beeswax.Models
{
    [SQLite.Table("producto")]
    public class Producto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string nombreProducto { get; set; }
        public string descripcion { get; set; }
        public float precio { get; set; }
        public string categoria { get; set; }
        public int stock { get; set; }
        public string imagen { get; set; }

    }
}
