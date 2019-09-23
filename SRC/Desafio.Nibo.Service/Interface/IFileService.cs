using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Nibo.Service
{
    public interface IFileService
    {
        Task<IEnumerable<string>> Save(List<IFormFile> files, string path);
    }
}
