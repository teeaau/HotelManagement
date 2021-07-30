using HotelManagement.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.UI.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>
    {
        public CustomerRepository() : base() { }
    }
}
