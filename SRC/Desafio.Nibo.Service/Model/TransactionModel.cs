using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Desafio.Nibo.Service.Model
{    
    public class TransactionModel
    {        
        [XmlElement("TRNTYPE")]
        public string Type { get; set; }
        [XmlElement("DTPOSTED")]
        public string Date { get; set; }
        [XmlElement("TRNAMT")]
        public string Value { get; set; }
        [XmlElement("MEMO")]
        public string Description { get; set; }
    }
}
