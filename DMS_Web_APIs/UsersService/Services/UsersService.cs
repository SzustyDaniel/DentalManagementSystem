using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersService.Data;

namespace UsersService.Services
{
    public class UsersService : IUsersService
    {
        private readonly UsersContext _usersContext;

        public UsersService(UsersContext usersContext)
        {
            _usersContext = usersContext;
        }
    }
}
