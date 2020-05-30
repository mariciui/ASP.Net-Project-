using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tema2.Data;

namespace Tema2.Services
{
    public class UserService 
    {
        private UnitOfWork unitOfWork;
        IUserService _userService;
        public UserService(IUserService userService, ApplicationDbContext context)
        {
            _userService = userService;
            unitOfWork = new UnitOfWork(context);
  
        }
        public int GetNrOfEmp()
        {
            return unitOfWork.UserRepository.GetNrOfEmp();
        }
    }
}
