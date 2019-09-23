using Desafio.Nibo.Domain.Entity;
using Desafio.Nibo.Domain.Interface;
using Desafio.Nibo.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio.Nibo.Domain.Repository
{
    public class TransactionRepository : IRepository<Transaction>
    {        
        public List<Transaction> Transactions;
        private MongoDbContext<Transaction> context;

        public TransactionRepository(string connectionString, string database)
        {
            context = new MongoDbContext<Transaction>(connectionString, database, "transaction");
            Transactions = new List<Transaction>();
        }
        public void Insert()
        {
            context.Insert(Transactions);
        }

        public void Update(string id)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            context.Delete(id);
        }

        public void DeleteAll()
        {
            context.DeleteAll();
        }

        public IEnumerable<Transaction> Get()
        {
            return context.Find();
        }

        public Transaction GetById(string id)
        {
            return context.FindById(id);
        }
    }
}
