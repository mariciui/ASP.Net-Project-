using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Tema2.Models;

namespace Tema2.Data
{
    public class UserRepository : GenericRepository<ApplicationUser>
    {

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public List<ApplicationUser> GetAllUsers()
        {
            return context.ApplicationUser.ToList();
        }

        public int GetNrOfEmp()
        {
            var list = context.ApplicationUser.ToList();
            List<ApplicationUser> toReturn = new List<ApplicationUser>();
            foreach (var user in list)
            {
                if (user.Role.Equals("Employee"))
                    toReturn.Add(user);
            }

            return toReturn.Count();
        }

    }
}
