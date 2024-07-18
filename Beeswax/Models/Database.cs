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
            await _database.CreateTableAsync<Compra>();
            await _database.CreateTableAsync<CompraDetalle>();
        }

        public static async Task<int> SaveCompraAsync(Compra compra)
        {
            try
            {
                await Init();
                return await _database.InsertAsync(compra);
            }
                

            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar la compra: {ex.Message}");
                return 0;
            }
        }

        public static Task<int> SaveCompraDetalleAsync(CompraDetalle detalle)
        {
            return _database.InsertAsync(detalle);
        }

        public static Task<List<Compra>> GetComprasAsync()
        {
            return _database.Table<Compra>().ToListAsync();
        }

        public static Task<List<CompraDetalle>> GetCompraDetallesAsync(int compraId)
        {
            return _database.Table<CompraDetalle>().Where(cd => cd.CompraId == compraId).ToListAsync();
        }

        public static Task<Producto> GetProductoAsync(int id)
        {
            return _database.Table<Producto>().FirstOrDefaultAsync(i => i.Id == id);
        }

        public static Task<int> DeleteCompraAsync(int id)
        {
            return _database.DeleteAsync<Compra>(id);
        }

    }
}

