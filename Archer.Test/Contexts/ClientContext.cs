﻿using Archer.Test.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using Archer.Test.Helpers;
using System.Data.Entity.Validation;

namespace Archer.Test.Contexts
{
    public class ClientContext :DbContext
    {
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
