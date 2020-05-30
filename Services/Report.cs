using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Threading.Tasks;
using Tema2.Models;

namespace Tema2.Services
{
    public interface Report
    {
        public void Export(List<Appointment> list);
    }
}
