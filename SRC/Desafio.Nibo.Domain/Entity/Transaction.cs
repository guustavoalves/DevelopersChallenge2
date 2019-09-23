using Desafio.Nibo.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio.Nibo.Domain.Entity
{
    public class Transaction : BaseEntity
    {
        public string FileName { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public TypeTransaction Type { get; set; }
    }

    public enum TypeTransaction
    {
        DEBIT = 1,
        CREDIT = 2
    }
}
