using Archer.Test.Helpers;
using Archer.Test.IOWorkers;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Archer.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            EntityHandler handler = new EntityHandler();
            ClearDBTables(handler, false, false);
            while (true)
            {

                string companyName = DataGenerator.GetCompanyName("");
                if (handler.CreateNewClient(companyName, "Archer.Test.IOWorkers.IOToCSV"))
                {
                    WriteToConsole("\"" + companyName + "\" Created");
                }

                companyName = DataGenerator.GetCompanyName(companyName);
                if (handler.CreateNewClient(companyName, "Archer.Test.IOWorkers.IOToJson"))
                {
                    WriteToConsole("\"" + companyName + "\" Created");
                }

                WriteToConsole("");

                foreach (var client in handler.GetClients())
                {
                    IIOBase ioWorker = IOFactory.Instance.GetIOWorker(client.IOWorkerName);
                    WriteToConsole("Generation random data for \"" + client.Name + "\"");
                    DataGenerator.Generate(ioWorker, Config.Instance.TestRecordsCount);
                    WriteToConsole("\tExporting data for \"" + client.Name + "\" as " + ioWorker.Type);
                    ioWorker.Export(client.Name + "_" + client.Id);

                }

                WriteToConsole("");

                foreach (var client in handler.GetClients())               
                {
                    IIOBase ioWorker = IOFactory.Instance.GetIOWorker(client.IOWorkerName);
                    WriteToConsole("Importing data file for \"" + client.Name + "\"");
                    ioWorker.Import(client.Name + "_" + client.Id);
                    if (Config.Instance.DeleteFileAfterImport)
                    {
                        WriteToConsole("\tClearing file from directory");
                        ioWorker.DeleteFile(client.Name + "_" + client.Id);
                    }
                    WriteToConsole("\tCommitting data for \"" + client.Name + "\" to database");
                    handler.AddClientRecords(client.Id, ioWorker.Data);
                }

                WriteToConsole("");

                foreach (var client in handler.GetClients())
                {
                    WriteToConsole("Cell number test result for \"" + client.Name + "\" (" + handler.GetTotalEntries(client.Id) + " records)");
                    string[] result = handler.TestCellnumbers(client.Id);
                    for (int i = 0; i < result.Length; i++)
                    {
                        WriteToConsole(result[i]);
                    }
                }

                Console.WriteLine("");
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine("Press 1 to re-run...");
                Console.WriteLine("Press 2 to clean & re-run...");
                Console.WriteLine("Press 3 to clean & exit...");
                Console.WriteLine("Press any other key to exit...");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        Console.Clear();
                        ClearDBTables(handler, false, false);
                        break;
                    case '2':
                        Console.Clear();
                        ClearDBTables(handler, true, true);
                        break;
                    case '3':
                        Console.Clear();
                        ClearDBTables(handler, true, true);
                        Environment.Exit(0);
                        break;
                    default:
                        Environment.Exit(0);
                        break;

                }
            }
        }

        private static void ClearDBTables(EntityHandler handler, bool clearDirectory, bool force)
        {
            if (Config.Instance.ClearTablesBeforeRun || force)
            {
                WriteToConsole("Clearing data from database");
                handler.ClearTables();

                WriteToConsole("Clearing file from directory");
                foreach (var item in Directory.GetFiles(Config.Instance.DropLocation))
                {
                    if (File.Exists(item))
                    {
                        try
                        {
                            File.Delete(item);
                        }
                        catch
                        {
                            //swallow exception
                        }
                    }
                }
                WriteToConsole(""); 
            }
        }
        private static void WriteToConsole(string message)
        {
            Console.WriteLine("{0:HH:mm:ss} :\t{1}", DateTime.Now, message);
        }

    }
}
