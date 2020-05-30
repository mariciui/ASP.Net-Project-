using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tema2.Models;

namespace Tema2.Services
{
    public interface IAppointmentService
    {
        int Id { get; set; }
        string Client { get; set; }
        public string getClientByIdTest(int id);

    }
}
