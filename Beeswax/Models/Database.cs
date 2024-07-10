using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Beeswax.Models
{
    public class Database
    {
        private static SQLiteAsyncConnection _database;

        public static async Task Init()
        {
            if (_database != null)
                return;

            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Beeswax.db");
            _database = new SQLiteAsyncConnection(databasePath);
            await _database.CreateTableAsync<Producto>();
        }

        public static Task<List<Producto>> GetProductosAsync()
        {
            return _database.Table<Producto>().ToListAsync();
        }

        public static Task<Producto> GetProductoAsync(int id)
        {
            return _database.Table<Producto>().FirstOrDefaultAsync(i => i.Id == id);
        }

        public static Task<int> SaveProductoAsync(Producto producto)
        {
            if (producto.Id != 0)
            {
                return _database.UpdateAsync(producto);
            }
            else
            {
                return _database.InsertAsync(producto);
            }
        }

        public static Task<int> DeleteProductoAsync(Producto producto)
        {
            return _database.DeleteAsync(producto);
        }
    }
}
