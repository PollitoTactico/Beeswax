using SQLite;
using System;
using System.Collections.Generic;

namespace Beeswax.Models
{
    [Table("compra")]
    public class Compra
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public float Total { get; set; }

        

        [Ignore]
        public List<CompraDetalle> Detalles { get; set; }
    }

    [Table("compradetalle")]
    public class CompraDetalle
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int CompraId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public float Precio { get; set; }
    }
}
