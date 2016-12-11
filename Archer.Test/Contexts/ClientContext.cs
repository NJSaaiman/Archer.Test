using Archer.Test.Helpers;
using Archer.Test.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace Archer.Test.Contexts
{
    public class ClientContext : DbContext
    {
        public ClientContext() : base(Config.Instance.DBName) { }
        public DbSet<Client> Clients { get; set; }

        public DbSet<ClientData> Data { get; set; }

        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            var e = entityEntry.Entity as ClientData;
            if (e != null)
            {
                if (Validations.IsCellNUmberValid(e.CellNumber))
                {
                    e.IsValid = true;
                }
                else
                {
                    e.IsValid = false;
                }
            }

            return base.ValidateEntity(entityEntry, items);
        }
    }
}
