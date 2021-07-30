using HotelManagement.Database;
using HotelManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.UI.Repositories
{
    public class UserRepository:GenericRepository<User>
    {
        public UserRepository():base() { }
    }
}
