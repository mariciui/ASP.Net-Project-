using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Server.HttpSys;
using Tema2.Data;
using Tema2.Models;

namespace Tema2.Services
{
    public class AppointmentService 
    {
       private UnitOfWork unitOfWork;
       private Stack<Report> reportStack = new Stack<Report>();
       SendEmail sendEmail;
        IAppointmentService appointmentService;


        public AppointmentService(ApplicationDbContext context)
        {
            unitOfWork = new UnitOfWork(context);
            sendEmail = new SendEmail();
        }

        public AppointmentService(ApplicationDbContext context, IAppointmentService appointmentService)
        {
            unitOfWork = new UnitOfWork(context);
            appointmentService = appointmentService;
        }


        public List<Appointment> GetAllAppointments()
        {
            return unitOfWork.AppointmentRepository.GetAllAppointments();
        }
        public List<Appointment> Index()
        {
            return (unitOfWork.AppointmentRepository.Get()).ToList();
        }

        public Appointment Details(int id)
        {
            return unitOfWork.AppointmentRepository.GetByID(id);
        }

        public void Create(Appointment appointment)
        {
           var clients = unitOfWork.UserRepository.GetAllUsers();
            foreach (var c in clients)
                if (appointment.Client.Equals(c.FirstName) && c.Role.Equals("Customer"))
                {
                    if (c.Email != null)
                    {
                        sendEmail.SEmail(c.Email, "Appointment", "New Appointment");
                        unitOfWork.AppointmentRepository.Insert(appointment);
                    }
                }
                
        }

        public void Edit(Appointment appointment)
        {
           unitOfWork.AppointmentRepository.Update(appointment);
        }

        public void Delete(int ID)
        {
            unitOfWork.AppointmentRepository.Delete(ID);
        }
        public void Save()
        {
            unitOfWork.AppointmentRepository.Save();
        }

        public Appointment getByID(int id)
        {
            return unitOfWork.AppointmentRepository.GetByID(id);
        }

        public List<Appointment> showAppointments(DateTime date)
        {
            var appointments = unitOfWork.AppointmentRepository.Get();
            List<Appointment> toReturn = new List<Appointment>(); 
            foreach (var a in appointments)
            {
                if (a.Date.Date.Equals(date.Date))
                    toReturn.Add(a);
            }

            return toReturn;
        }

        public List<Appointment> SearchBetweenDates(DateTime start, DateTime stop)
        {
            List<Appointment> toReturn = new List<Appointment>();
            var appointments = unitOfWork.AppointmentRepository.Get();

            foreach (var a in appointments)
            {
                if (a.Date.Date > start.Date && a.Date.Date < stop.Date )
                    toReturn.Add(a);
            }

            return toReturn;
        }
        public void checkAppointment(int ID)
        {

            Appointment appointment = unitOfWork.AppointmentRepository.GetByID(ID);

            appointment.Status = "YES";

            unitOfWork.AppointmentRepository.Update(appointment);

        }

        public List<Appointment> ShowAppointments(string Client)
        {
            var appointments = unitOfWork.AppointmentRepository.Get();
            List<Appointment> toReturn = new List<Appointment>();

           foreach (var a in appointments)
                    {
                        if (a.Client.Equals(Client))
                            toReturn.Add(a);
                    }
         
            return toReturn;
        }

        public void exportReport(List<Appointment> list, string type)
        {

            Report r = ReportFactory.Create(type);
            reportStack.Push(r);
            r.Export(list);
        }

        public string getClientByIdTestt(int id)
        {
            return appointmentService.Client;
        }

    }
}
