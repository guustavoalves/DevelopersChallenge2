using System;
using System.Collections.Generic;
using System.Text;
using Desafio.Nibo.Domain.Entity;
using Desafio.Nibo.Domain.Repository;

namespace Desafio.Nibo.Service
{
    public class TransactionService : ITransactionService
    {
        TransactionRepository repository;

        public TransactionService(string connectionString, string database)
        {
            repository = new TransactionRepository(connectionString, database);
        }

        public IEnumerable<Transaction> Get()
        {
            return repository.Get();
        }

        public void DeleteAll()
        {
            repository.DeleteAll();
        }
    }
}
