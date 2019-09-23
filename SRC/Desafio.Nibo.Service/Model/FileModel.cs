using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Desafio.Nibo.Service.Model
{
    [XmlRoot("BANKTRANLIST")]
    public class FileModel
    {
        [XmlIgnore]
        public string FileName { get; set; }

        [XmlElement("STMTTRN")]
        public List<TransactionModel> Transactions { get; set; }
    }
}
