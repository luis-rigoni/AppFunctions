using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using SQLite;
using AppFunctions.Models;

namespace AppFunctions.Helpers
{
    public class SQLiteDatabaseHelpers
    {

        readonly SQLiteAsyncConnection _connection;

        public SQLiteDatabaseHelpers(string path) { 
            _connection = new SQLiteAsyncConnection(path);
            _connection.CreateTableAsync<Especie>().Wait();
        }

            public Task<int> Insert(Especie p) {
                return _connection.InsertAsync(p);
            }

            public Task<List<Especie>> Update(Especie p) {
                string sql = "UPDATE Especie SET Nome=? WHERE Codigo=?";
                return _connection.QueryAsync<Especie>(sql, p.Nome, p.Codigo);
            }

            public Task<List<Especie>> Delete(int p) {

                // _connection.Table<Especie>().DeleteAsync(i => i.Codigo == p);

                string sql = "DELETE Especie WHERE Codigo=?";
                return _connection.QueryAsync<Especie>(sql, p);
            }

            public Task<List<Especie>> GetAll() {

                return _connection.Table<Especie>().ToListAsync();

                //string sql = "SELECT * FROM Especie";
                //return _connection.QueryAsync<Especie>(sql, p);

            }

            public Task<List<Especie>> Search(string p) {
                string sql = "SELECT * FROM Especie WHERE Nome LIKE %'" + p + "'% ";
                return _connection.QueryAsync<Especie>(sql);
            }

    }
}
