using Archer.Test.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archer.Test.Contexts
{
    public class ClientContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientData> Data { get; set; }
        public DbSet<DataMap> Map { get; set; }

    }
}
