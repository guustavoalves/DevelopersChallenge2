using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Desafio.Nibo.UI.Web.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Hosting;
using Desafio.Nibo.Service;
using Microsoft.Extensions.Options;

namespace Desafio.Nibo.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        ITransactionService _transactionService;
        IOfxFileService _ofxFileService;
        IFileService _fileService;

        public HomeController(ITransactionService transactionService, IOfxFileService ofxFileService, IFileService fileService)
        {
            this._transactionService = transactionService;
            this._ofxFileService = ofxFileService;
            this._fileService = fileService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {                        
            var ofxFiles = await _fileService.Save(files, Path.GetFullPath("OfxFiles"));
            var model = _ofxFileService.Import(ofxFiles.ToList());
            return View("Imported", model);            
        }

        public IActionResult DeleteAll()
        {
            _transactionService.DeleteAll();
            return RedirectToAction(nameof(Extract));                       
        }

        public IActionResult Extract(string order, string search, string type)
        {
            ViewBag.CurrentSearch = search;
            ViewBag.CurrentType = type;

            var model = _transactionService.Get();

            switch (type)
            {
                case "1":
                    model = model.Where(m => m.Type.Equals(Domain.Entity.TypeTransaction.DEBIT));
                    break;
                case "2":
                    model = model.Where(m => m.Type.Equals(Domain.Entity.TypeTransaction.CREDIT));
                    break;
            }

            if (!String.IsNullOrEmpty(search))
            {
                model = model.Where(m => m.Description.ToUpper().Contains(search.ToUpper().Trim()));
            }

            switch (order)
            {
                case "1":
                    model = model.OrderBy(m => m.Date);
                    break;
                case "2":
                    model = model.OrderBy(m => m.Type);
                    break;
                case "3":
                    model = model.OrderBy(m => m.Description);
                    break;
                case "4":
                    model = model.OrderBy(m => m.Value);
                    break;
                default:
                    model = model.OrderByDescending(l => l.Date);
                    break;
            }

            return View(model);
        }

        public IActionResult Imported()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
