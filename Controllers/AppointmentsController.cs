using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tema2.Data;
using Tema2.Models;
using Tema2.Services;

namespace Tema2.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private AppointmentService appService;


        public AppointmentsController(ApplicationDbContext context)
        {
            _context = context;
            appService = new AppointmentService(context);
        }

        // GET: Appointments
        [Authorize(Roles = "Employee")]
        public ViewResult Index()
        {
       
            return View(appService.Index().ToList());
        }

        // GET: Appointments/Details/5
        [Authorize(Roles = "Employee")]
        public ViewResult Details(int id)
        {

            Appointment appointment = appService.Details(id);

            return View(appointment);
        }

        // GET: Appointments/Create
        [Authorize(Roles = "Employee")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Appointments/Create
        [Authorize(Roles = "Employee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,Date,Client,Phone,Car,Problem,Status")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                appService.Create(appointment);
                appService.Save();
                return RedirectToAction("Index");
            }
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        [Authorize(Roles = "Employee")]
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = appService.getByID(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employee")]
        public ActionResult Edit(int id, [Bind("Id,Date,Client,Phone,Car,Problem, Status")] Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    appService.Edit(appointment);
                    appService.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(appointment);
        }
        [Authorize(Roles = "Employee")]
        // GET: Appointments/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = appService.getByID(id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }
        [Authorize(Roles = "Employee")]
        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            appService.Delete(id);
            appService.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return (appService.getByID(id) != null);
        }
        [Authorize(Roles = "Employee")]
        public ActionResult Check(int id)
        {
            appService.checkAppointment(id);
           appService.Save();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Employee")]
        public ActionResult SearchClient(string Client)
        {
            var toReturn = appService.ShowAppointments(Client);
            return View(toReturn.ToList());
        }

        [Authorize(Roles = "Customer")]
        public ActionResult SearchClient1(string Client)
        {
            var toReturn = appService.ShowAppointments(Client);
            return View(toReturn.ToList());
        }

        [Authorize(Roles = "Employee")]
        public ActionResult SearchByDate(DateTime date)
        {
            var app = appService.Index();
            var toReturn = appService.showAppointments(date);
            return View(toReturn.ToList());
        }

        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> SearchBetweenDates(DateTime start, DateTime stop)
        { 
            var toReturn = appService.SearchBetweenDates(start,stop);
            return View(toReturn.ToList());
        }

        [Authorize(Roles = "Employee")]
        public ActionResult Export(int id)
        {

            if ( id == 1)
            {
                appService.exportReport(appService.GetAllAppointments(), "Json");
            }

            if (id == 2)
            {
                appService.exportReport(appService.GetAllAppointments(), "CSV");
            }
            return RedirectToAction(nameof(Index));
        }

    }
}

