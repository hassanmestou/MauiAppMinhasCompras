using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiAppMinhasCompras.Models;
using SQLite;

namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;

        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait();
        }

        public Task<int> Insert(Produto p)
        {
            return _conn.InsertAsync(p);
        }

        public Task<List<Produto>> Update(Produto p)
        {
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE Id=?";

            return _conn.QueryAsync<Produto>(sql, p.Descricao, p.Quantidade, p.Preco, p.Id);
        }

        //possivel erro no codigo anterior, quando usa o QueryAsync, que serve para consultas, dito isso
        //O mais certo conforme pesquisas seria usar o ExecuteAsybnc (para comandos UPDATE/DELETE/INSERT)
        


        public Task<int> Delete(int id) {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        
        }
        
        
        //possivel erro no vo
        //como o delete async é um a tarefa assincrona o return naop pdoe ser um void tem que ser umas task pois na verdade é uma terefa

        public Task<List<Produto>> GetAll() {
            return _conn.Table<Produto>().ToListAsync();
        
        }
        
        public Task<List<Produto>> Search(string q) {
            String sql = "SELECT * FROM Produto WHERE descricao LIKE '%"+ q +"%'";

            return _conn.QueryAsync<Produto>(sql);
        
        
        }
    }
}
