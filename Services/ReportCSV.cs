using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Text;
using Tema2.Models;

namespace Tema2.Services
{
    public class ReportCSV : Report
    {
        public void Export(List<Appointment> list)
        {
           
            string path = "CSV.csv";

           string aux = CsvSerializer.SerializeToCsv(list);


            File.WriteAllText(path, aux);
        }
    }

    }

