using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using TransactionDEMO_WEB.Models;

namespace TransactionDEMO_WEB.Helper
{
    public static class FileHelper
    {
        public static List<string> ValidateUploadFile(IFormFile file, string extension)
        {
            List<string> errors = new List<string>();


            if (file.Length > 1048576)
            {
                errors.Add("File size larger than 1MB");
            }


            if (extension != ".csv" && extension != ".xml")
            {
                errors.Add("Unknown file type");
            }

            return errors;
        }

        public static List<Transaction> ExtractXML(IFormFile file)
        {
            var result = new List<Transaction>();
            using (var fileStream = new StreamReader(file.OpenReadStream()))
            {
                string xmlString = fileStream.ReadToEnd();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlString);

                string jsonStr = JsonConvert.SerializeXmlNode(xmlDoc);

                var xmlDto = JsonConvert.DeserializeObject<TransactionXmlDto>(jsonStr);

                foreach (var transaction in xmlDto.Transactions.Transaction)
                {
                    var transItem = new Transaction()
                    {
                        Id = Guid.NewGuid(),
                        TransactionId = transaction.Id,
                        Amount = Convert.ToDecimal(transaction.PaymentDetails.Amount),
                        CurrencyCode = transaction.PaymentDetails.CurrencyCode,
                        TransactionDate = transaction.TransactionDate,
                        Status = ConversionHelper.ConvertStatus(transaction.Status)
                    };

                    result.Add(transItem);
                }

                return result;
            }
        }

        public static List<Transaction> ExtractCSV(IFormFile file)
        {
            var result = new List<Transaction>();

            using (var streamReader = new StreamReader(file.OpenReadStream()))
            {
                string line = string.Empty;
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] strRow = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                    DateTime transDate = new DateTime();

                    transDate = Convert.ToDateTime(TrimQuotes(strRow[3]));

                    var tranItem = new Transaction()
                    {
                        Id = Guid.NewGuid(),
                        TransactionId = TrimQuotes(strRow[0]),
                        Amount = Convert.ToDecimal(TrimQuotes(strRow[1])),
                        CurrencyCode = TrimQuotes(strRow[2]),
                        TransactionDate = transDate,
                        Status = ConversionHelper.ConvertStatus(TrimQuotes(strRow[4]))
                    };

                    result.Add(tranItem);
                }

            }

            return result;
        }

        public static string TrimQuotes(string text)
        {
            return text.TrimStart('"').TrimEnd('"');
        }
    }
}
