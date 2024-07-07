using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Beeswax.Models
{
    [SQLite.Table("producto")]
    public class Producto
    {
        [PrimaryKey , AutoIncrement]
        public int Id { get; set; }
        public string nombreProducto { get; set; }
        public string descripcion { get; set; }
        public int precio   { get; set; }
        public string categoria { get; set; }
        public int stock { get; set; }
        public string imagen { get; set; }

    }
}
