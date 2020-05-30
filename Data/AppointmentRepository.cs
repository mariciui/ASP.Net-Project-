using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tema2.Models;

namespace Tema2.Data
{
    public class AppointmentRepository : GenericRepository<Appointment>
    {
      public AppointmentRepository(ApplicationDbContext context):base(context)
        {
            this.context = context;
        }

        public List<Appointment> GetAllAppointments()
        {
            return context.Appointment.ToList();
        }

    }
}
