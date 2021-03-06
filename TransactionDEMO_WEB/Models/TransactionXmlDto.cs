using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionDEMO_WEB.Models
{
    public class Xml
    {
        [JsonProperty("@version")]
        public string Version { get; set; }
        [JsonProperty("@encoding")]
        public string Encoding { get; set; }
    }

    public class PaymentDetails
    {
        public string Amount { get; set; }
        public string CurrencyCode { get; set; }
    }

    public class Tran
    {
        [JsonProperty("@id")]
        public string Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public PaymentDetails PaymentDetails { get; set; }
        public string Status { get; set; }
    }

    public class Transactions
    {
        public List<Tran> Transaction { get; set; }
    }

    public class TransactionXmlDto
    {
        [JsonProperty("?xml")]
        public Xml Xml { get; set; }
        public Transactions Transactions { get; set; }
    }
}
