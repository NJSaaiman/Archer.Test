using Archer.Test.Contexts;
using Archer.Test.Helpers;
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
                Console.WriteLine("Creating Company A");
                var blog = new Client { Name = "Company A" };
                db.Clients.Add(blog);
                Console.WriteLine("Creating Company B");
                blog = new Client { Name = "Company B" };
                db.Clients.Add(blog);
                db.SaveChanges();
            }

            Console.WriteLine("Creating data file for Company A");
            Randomizer rnd = new Randomizer();
            for (int i = 0; i < 10000; i++)
            {
                string tel = rnd.RandomCellNumber;
                string name = rnd.RandomName;
                Console.WriteLine("{0} : {1} \t {2} ({3}) \t {4}", i+1, name, tel, tel.Length, rnd.GetEmailAddress(name));
            }

            Console.WriteLine("Creating data file for Company B");
            Randomizer rnd2 = new Randomizer();
            for (int i = 0; i < 10000; i++)
            {
                string tel = rnd2.RandomCellNumber;
                string name = rnd2.RandomName;
                Console.WriteLine("{0} : {1} \t {2} ({3}) \t {4}", i+1, name, tel, tel.Length, rnd2.GetEmailAddress(name));
            }


            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();


            
        }

    }
}
