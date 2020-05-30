using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Tema2.Models;
using System.IO;

namespace Tema2.Services
{
    public class ReportJSON : Report
    {
        string jsonString;
       
        public void Export(List<Appointment> list)
        {
            string path = "Json";
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            jsonString = JsonSerializer.Serialize(list, options);
            File.WriteAllText(path, jsonString);
        }
    }
}
