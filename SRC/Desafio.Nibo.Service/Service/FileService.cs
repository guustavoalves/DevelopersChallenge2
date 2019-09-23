using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Desafio.Nibo.Service
{
    public class FileService : IFileService
    {
        public async Task<IEnumerable<string>> Save(List<IFormFile> files, string path)
        {
            var ofxFiles = new List<string>();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {                    
                    using (var stream = new FileStream(Path.Combine(path, formFile.FileName), FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
                ofxFiles.Add(Path.Combine(path, formFile.FileName));
            }
            return ofxFiles;
        }
    }
}
