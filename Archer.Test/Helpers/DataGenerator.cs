using Archer.Test.DTO;
using Archer.Test.IOWorkers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archer.Test.Helpers
{
    public static class DataGenerator
    {
        public static void Generate(IIOBase ioWorker, int recordsToGenerate)
        {
            Randomizer rnd = new Randomizer();
            for (int i = 0; i < recordsToGenerate; i++)
            {
                string tel = rnd.RandomCellNumber;
                string name = rnd.RandomName;
                ioWorker.AddRecord(new ClientDataDTO() { Name = name, CellNumber = tel, EmailAddress = rnd.GetEmailAddress(name) });
            }
        }

        public static string GetCompanyName(string companyName)
        {
            Randomizer rnd = new Randomizer();
            return rnd.GetCompanyName(companyName);
        }
    }
}
