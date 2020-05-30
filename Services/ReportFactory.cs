using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tema2.Services
{
    public class ReportFactory
    {
        public enum ReportTypes { Json, CSV};

        public static Report Create(string type)
        {
            Report report = null;

            if (type.Equals(ReportTypes.CSV.ToString()))
            {
                report = new ReportCSV();
                return report;
            }
            else if (type.Equals(ReportTypes.Json.ToString()))
            {
                report = new ReportJSON();
                return report;
            }
            return report;
        }
    }
}
