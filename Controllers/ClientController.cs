using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ServiceStack;
using Tema2.Models;

namespace Tema2.Controllers
{
    public class ClientController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ClientController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [Authorize(Roles ="Customer")]
        public IActionResult Index()
        {
            var current_User = _userManager.GetUserId(HttpContext.User);
            if (current_User == null)
            {
                return View();
            }
            else
            {
                ApplicationUser user = _userManager.FindByIdAsync(current_User).Result;

                return RedirectToAction("SearchClient1", new RouteValueDictionary(
                                     new { controller = "Appointments", action = "SearchClient1", Client = user.FirstName }));
            }

        }

   

    }
}