using System;
using System.Collections.Generic;
using System.Text;
using Desafio.Nibo.Domain.Entity;

namespace Desafio.Nibo.Service
{
    public interface IOfxFileService
    {
        IEnumerable<Transaction> Import(List<string> files);
    }
}
