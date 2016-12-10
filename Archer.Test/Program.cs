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


            EntityHandler handler = new EntityHandler();
            while (true)
            {
                string companyName = DataGenerator.GetCompanyName(); // "Stark Industries";
                if (handler.CreateNewClient(companyName, "Archer.Test.IOWorkers.IOToCSV"))
                {
                    WriteToConsole(companyName + " Created");
                }

                companyName = DataGenerator.GetCompanyName();  //"Wonka Industries";
                if (handler.CreateNewClient("Wonka Industries", "Archer.Test.IOWorkers.IOToJson"))
                {
                    WriteToConsole(companyName + " Created");
                }

                WriteToConsole("");

                foreach (var client in handler.GetClients())
                {
                    IIOBase ioWorker = IOFactory.GetIOWorker(client.IOWorkerName);
                    WriteToConsole("Generation random data for " + client.Name);
                    DataGenerator.Generate(ioWorker, 100);
                    WriteToConsole("\tExporting data for " + client.Name + " as " + ioWorker.Type);
                    ioWorker.Export(client.Name);

                }

                WriteToConsole("");

                foreach (var client in handler.GetClients())
                {
                    IIOBase ioWorker = IOFactory.GetIOWorker(client.IOWorkerName);
                    WriteToConsole("Importing data file for " + client.Name);
                    ioWorker.Import(client.Name);
                    WriteToConsole("\tCommitting data for " + client.Name + " to database");
                    handler.AddClientRecords(client.Name, ioWorker.Data);
                }

                WriteToConsole("");

                foreach (var client in handler.GetClients())
                {
                    WriteToConsole("Cell number test result for " + client.Name);
                    string[] result = handler.TestCellnumbers(client.Name);
                    WriteToConsole(result[0]);
                    WriteToConsole(result[1]);
                }

                Console.WriteLine("");
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine("Press 1 key to re-run...");
                Console.WriteLine("Press any other key to exit...");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        Console.Clear();
                        handler.ClearTables();
                        break;
                    default:
                        Environment.Exit(0);
                        break;

                }
            }



        }




        private static void WriteToConsole(string message)
        {
            Console.WriteLine("{0:HH:mm:ss} :\t{1}", DateTime.Now, message);
        }

    }
}
