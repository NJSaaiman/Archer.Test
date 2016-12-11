using Archer.Test.Contexts;
using Archer.Test.DTO;
using Archer.Test.Helpers;
using Archer.Test.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;

namespace Archer.Test
{
    public class EntityHandler
    {
        public EntityHandler()
        {
            Initialise();
        }

        protected virtual void Initialise()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ClientContext>());
        }

        public void ClearTables()
        {
            using (var db = GetContext())
            {
                db.Data.RemoveRange(db.Data);
                db.Clients.RemoveRange(db.Clients);
                db.SaveChanges();
            }
        }

        public bool CreateNewClient(string name, string ioWorker)
        {
            using (var db = GetContext())
            {
                db.Clients.Add(new Models.Client() { Name = name, IOWorkerName = ioWorker });
                return db.SaveChanges() > 0;
            }
        }

        public List<Client> GetClients()
        {
            using (var db = GetContext())
            {
                var tmpList = (from b in db.Clients
                               orderby b.Id
                               select b).ToList();

                return tmpList;
            }
        }

        public void AddClientRecords(int clientId, List<ClientDataDTO> clientData)
        {
            using (var db = GetContext())
            {
                try
                {
                    db.Configuration.AutoDetectChangesEnabled = false;
                    var importedData = clientData.Select(x => new ClientData() { Name = x.Name, EmailAddress = x.EmailAddress, CellNumber = x.CellNumber }).ToList();
                    var client = (from c in db.Clients
                                  where c.Id == clientId
                                  select c).DefaultIfEmpty(null).FirstOrDefault();

                    if (client != null)
                    {
                        client.Data.AddRange(importedData);
                        db.Configuration.AutoDetectChangesEnabled = true;
                        db.SaveChanges();
                    }
                }
                finally
                {
                    db.Configuration.AutoDetectChangesEnabled = true;
                }
            }

        }

        public int GetTotalEntries(int clientId)
        {
            using (var db = GetContext())
            {

                db.Configuration.AutoDetectChangesEnabled = false;

                var count = (from c in db.Clients
                             where c.Id == clientId
                             let data = c.Data
                             from d in data
                             select d).Count();
                return count;
            }
        }

        public string[] TestCellnumbers(int clientId)
        {
            using (var db = GetContext())
            {

                db.Configuration.AutoDetectChangesEnabled = false;

                var client = (from c in db.Clients
                              where c.Id == clientId
                              let data = c.Data
                              from d in data
                              select d);

                var invalidClients = client.Where(x => !x.IsValid);
                int inValidCount = invalidClients.AsEnumerable().Count();

                int shorNumbersCount = 0;
                int notNumericCount = 0;
                if (inValidCount > 0)
                {

                    shorNumbersCount = (from c in invalidClients.AsEnumerable()
                                        where c.CellNumber.Length != 10
                                        select c).Count();

                    notNumericCount = (from c in invalidClients.AsEnumerable()
                                       where !Validations.IsNumeric(c.CellNumber)
                                       select c).Count();
                }

                int valid = (from c in client
                             where c.IsValid
                             select c).Count();

                return new string[4] { string.Format("\tFound {0} invalid cell numbers.", inValidCount),
                                        string.Format("\t\t{0} cell numbers are not 10 digits.", shorNumbersCount),
                                        string.Format("\t\t{0} cell numbers are not numeric.", notNumericCount),
                                        string.Format("\tFound {0} valid cell numbers.", valid) };



            }
        }

        private ClientContext GetContext()
        {
            return new ClientContext();
        }

    }


}
