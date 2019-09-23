using System;
using System.Collections.Generic;
using System.Text;
using Desafio.Nibo.Domain.Entity;

namespace Desafio.Nibo.Service
{
    public interface ITransactionService
    {
        IEnumerable<Transaction> Get();
        void DeleteAll();
    }
}
