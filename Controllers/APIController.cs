using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tema2.Data;
using Tema2.Models;
using Tema2.Services;

namespace Tema2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKeyAuth]
    public class APIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private AppointmentService appService;

        public APIController(ApplicationDbContext context)
        {
            _context = context;
            appService = new AppointmentService(context);
        }

        // GET: api/API
        [HttpGet]
        public string GetAppointment()
        {
            var app = appService.GetAllAppointments();

            List<Appointment> toReturn = app.OrderBy(a => a.Date).ToList();
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            string jsonString = JsonSerializer.Serialize(toReturn, options);
          
            return jsonString;

        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientAppointment(string id)
        {
            var app = appService.ShowAppointments(id);
            return Ok(app);
        }

    }
}
