using Archer.Test.Contexts;
using Archer.Test.DTO;
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
            Database.SetInitializer(new DropCreateDatabaseAlways<ClientContext>());
        }

        public void ClearTables()
        {
            using (var db = new ClientContext())
            {
                db.Data.RemoveRange(db.Data);
                db.Clients.RemoveRange(db.Clients);
                db.SaveChanges();
            }
        }

        public bool CreateNewClient(string name, string ioWorker)
        {
            using (var db = new ClientContext())
            {
                db.Clients.Add(new Models.Client() { Name = name, IOWorkerName = ioWorker });
                return db.SaveChanges() > 0;
            }
        }

        public List<Client> GetClients()
        {
            using (var db = new ClientContext())
            {
                var tmpList = (from b in db.Clients
                               orderby b.Name
                               select b).ToList();

                return tmpList;
            }
        }

        public void AddClientRecords(string clientName, List<ClientDataDTO> clientData)
        {
            using (var db = new ClientContext())
            {
                var importedData = clientData.Select(x => new ClientData() { Name = x.Name, EmailAddress = x.EmailAddress, CellNumber = x.CellNumber, IsValid = IsCellNUmberValid(x.CellNumber) }).ToList();
                var client = (from c in db.Clients
                              where c.Name == clientName
                              select c).DefaultIfEmpty(null).FirstOrDefault();

                if (client != null)
                {
                    client.Data.AddRange(importedData);
                    db.SaveChanges();
                }
            }

        }

        public string[] TestCellnumbers(string clientName)
        {
            using (var db = new ClientContext())
            {
                db.Configuration.AutoDetectChangesEnabled = false;
                
              
                int inValid = (from c in db.Clients
                               where c.Name == clientName
                               let data = c.Data
                               from d in data
                               where d.IsValid == false
                               select d).Count();

                int valid = (from c in db.Clients
                             where c.Name == clientName
                             let data = c.Data
                             from d in data
                             where d.IsValid == true
                             select d).Count();

                return new string[2] { string.Format("\t{0} invalid cell numbers.", inValid), string.Format("\t{0} valid cell numbers", valid) };



            }
        }

        private bool IsCellNUmberValid(string cellNumber)
        {
            if (cellNumber.Length != 10) { return false; }

            return Regex.IsMatch(cellNumber, "^[0-9]*$", RegexOptions.CultureInvariant);

        }

    }


}
