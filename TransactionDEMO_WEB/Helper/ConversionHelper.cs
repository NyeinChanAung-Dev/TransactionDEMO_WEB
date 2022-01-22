using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionDEMO_WEB.Helper
{
    public static class ConversionHelper
    {
        public static string ConvertStatus(string status)
        {
            switch (status)
            {
                case "Approved":
                    return "A";
                case "Failed":
                case "Rejected":
                    return "R";
                case "Finished":
                case "Done":
                    return "D";
                default:
                    return "";
            }
        }
    }

}
