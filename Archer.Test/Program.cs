using Archer.Test.Contexts;
using Archer.Test.Helpers;
using Archer.Test.IOWorkers;
using Archer.Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archer.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new ClientContext())
            {
                db.Data.RemoveRange(db.Data);
                db.Clients.RemoveRange(db.Clients);
                Console.WriteLine("Creating Company A");
                var blog = new Client { Name = "Company A", IOWorkerName = "Archer.Test.IOWorkers.IOClientA" };
                db.Clients.Add(blog);
                Console.WriteLine("Creating Company B");
                blog = new Client { Name = "Company B", IOWorkerName = "Archer.Test.IOWorkers.IOClientB" };
                db.Clients.Add(blog);
                db.SaveChanges();

                var q = from a in db.Clients
                        orderby a.Name
                        select a;

                Randomizer rnd = new Randomizer();
                List<IIOBase> ioWorkers = new List<IIOBase>();
                foreach (var client in q)
                {
                    Console.WriteLine("Creating data file for " + client.Name);
                    IIOBase ioWorker = IOFactory.GetIOWorker(client.IOWorkerName);
                    for (int i = 0; i < 100; i++)
                    {
                        string tel = rnd.RandomCellNumber;
                        string name = rnd.RandomName;
                        ioWorker.AddRecord(new DTO.ClientDataDTO() { Name = name, CellNumber = tel, EmailAddress = rnd.GetEmailAddress(name) });
                        //Console.WriteLine("{0} : {1} \t {2} ({3}) \t {4}", i+1, name, tel, tel.Length, rnd.GetEmailAddress(name));
                    }
                    ioWorker.Export(client.Name);
                    ioWorkers.Add(ioWorker);
                }
            }

            
            

            
            //IIOBase clientA = IOFactory.GetIOWorker("IOClientA");
            //Randomizer rnd = new Randomizer();
            
            //for (int i = 0; i < 100; i++)
            //{
            //    string tel = rnd.RandomCellNumber;
            //    string name = rnd.RandomName;
            //    clientA.AddRecord(new DTO.ClientDataDTO() { Name = name, CellNumber = tel, EmailAddress = rnd.GetEmailAddress(name) });
            //    //Console.WriteLine("{0} : {1} \t {2} ({3}) \t {4}", i+1, name, tel, tel.Length, rnd.GetEmailAddress(name));
            //}

            //Console.WriteLine("Creating data file for Company B");
            //IIOBase clientB = IOFactory.GetIOWorker("IOClientB");
            //Randomizer rnd2 = new Randomizer();
            //for (int i = 0; i < 100; i++)
            //{
            //    string tel = rnd2.RandomCellNumber;
            //    string name = rnd2.RandomName;
            //    clientB.AddRecord(new DTO.ClientDataDTO() { Name = name, CellNumber = tel, EmailAddress = rnd2.GetEmailAddress(name) });
            //    //Console.WriteLine("{0} : {1} \t {2} ({3}) \t {4}", i+1, name, tel, tel.Length, rnd2.GetEmailAddress(name));
            //}

            //clientA.Export("clientA.csv");
            //clientA.Import("clientA.csv");

            //clientB.Export("clientB.json");
            //clientB.Import("clientB.json");

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();


            
        }

    }
}
