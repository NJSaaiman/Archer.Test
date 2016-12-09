﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archer.Test.DTO;
using System.IO;

namespace Archer.Test.IOWorkers
{
    class IOClientA : IOBase
    {
        public IOClientA(List<ClientDataDTO> data) : base(data)
        {
        }
        public IOClientA() : base() { }

        public override void Export(string fileName)
        {
            StringBuilder sb = new StringBuilder();
            if (Data.Count > 0)
            {
                Data.ForEach(x => sb.AppendLine(string.Join(",", new string[] { x.Name, x.CellNumber, x.EmailAddress})));
            }

            File.WriteAllText(fileName+".csv", sb.ToString());

        }

        public override void Import(string fileName)
        {
            Data.Clear();

            Data.AddRange((from line in File.ReadAllLines(fileName)
                           let columns = line.Split(',')
                           select new ClientDataDTO
                           {
                               Name = columns[0],
                               CellNumber = columns[1],
                               EmailAddress = columns[2]
                           }).ToList());
        }
    }
}
