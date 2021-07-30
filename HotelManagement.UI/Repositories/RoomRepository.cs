using HotelManagement.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.UI.Repositories
{
    public class RoomRepository : GenericRepository<Room>
    {
        public RoomRepository() : base() { }
    }
}
