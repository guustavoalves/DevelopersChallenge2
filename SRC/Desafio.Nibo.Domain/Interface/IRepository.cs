using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio.Nibo.Domain.Interface
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Get();

        void Insert();

        void Update(string id);

        void Delete(string id);
    }
}
