using System;
using System.Collections.Generic;
using System.Text;
using Desafio.Nibo.Domain.Entity;
using Desafio.Nibo.Domain.Repository;
using Desafio.Nibo.Service.Model;
using System.IO;
using System.Xml.Serialization;
using System.Linq;

namespace Desafio.Nibo.Service
{
    public class OfxFileService : IOfxFileService
    {
        TransactionRepository repository;

        public OfxFileService(string connectionString, string database)
        {
            repository = new TransactionRepository(connectionString, database);
        }

        public IEnumerable<Transaction> Import(List<string> files)
        {            
            if (files.Count > 0)
            {
                var list = repository.Get();

                foreach (var file in files)
                {                    
                    var lines = File.ReadAllLines(file);
                    var fileOfx = Serializer(ParseToXml(lines));                    
                    
                    foreach (var item in fileOfx.Transactions)
                    {
                        var transaction = new Transaction
                        {
                            FileName = file.Substring(file.LastIndexOf(@"\") + 1),
                            Description = item.Description.Trim(),
                            Date = new DateTime(Int32.Parse(item.Date.Substring(0, 4)), Int32.Parse(item.Date.Substring(4, 2)), Int32.Parse(item.Date.Substring(6, 2))),
                            Type = (item.Type.Trim().Equals("DEBIT")) ? TypeTransaction.DEBIT : TypeTransaction.CREDIT,
                            Value = Convert.ToDecimal(item.Value.Trim().Replace(".",","))
                        };

                        //Checking if same transaction exists on different files
                        var runtine = repository.Transactions.Where(l => l.Description.Equals(transaction.Description) && l.Date.Equals(transaction.Date) && l.Type.Equals(transaction.Type) && l.Value.Equals(transaction.Value) && !l.FileName.Equals(transaction.FileName)).Any();

                        //Checking if transaction already registered in database
                        var database = list.Where(l => l.Description.Equals(transaction.Description) && l.Date.ToShortDateString().Equals(transaction.Date.ToShortDateString()) && l.Type.Equals(transaction.Type) && l.Value.Equals(transaction.Value)).Any();

                        if (!runtine && !database)
                        {
                            repository.Transactions.Add(transaction);
                        }
                    }                    
                }
                repository.Insert();
            }
            return repository.Transactions;
        }

        private string ParseToXml(string[] lines)
        {
            var tags = new string[] { "<TRNTYPE>", "<DTPOSTED>", "<TRNAMT>", "<MEMO>" };            
            var xml = new StringBuilder();

            foreach (var line in lines)
            {
                if (line.Contains("BANKTRANLIST") || line.Contains("STMTTRN>"))
                {                    
                    xml.Append(line);
                }
                
                if (tags.Any(line.Contains))
                {                    
                    xml.Append(line);
                    xml.Append("</" + line.Substring(1, line.IndexOf(">")));
                }
            }
            return xml.ToString();
        }

        private FileModel Serializer(string xml)
        {
            var serializer = new XmlSerializer(typeof(FileModel));
            return (FileModel)(serializer.Deserialize(new StringReader(xml)));
        }
    }
}
