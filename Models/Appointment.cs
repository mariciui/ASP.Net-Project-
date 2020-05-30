using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tema2.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        //[DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string Client { get; set; }
        public string Phone { get; set; }
        public string Car { get; set; }
        public string Problem { get; set; }

        public string Status { get; set; }
    }
}
