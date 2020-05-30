using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Tema2.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() : base()
        {
         
        }
        public string FirstName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public string Role { get; set; }

    }
}
