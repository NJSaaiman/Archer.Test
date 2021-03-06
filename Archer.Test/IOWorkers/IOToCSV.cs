﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archer.Test.DTO;
using System.IO;

namespace Archer.Test.IOWorkers
{
    class IOToCSV : IOBase
    {
        public IOToCSV(List<ClientDataDTO> data) : base(data) { }
        protected override string Extention
        {
            get
            {
                return ".csv";
            }
        }
        public override string Type
        {
            get
            {
                return "CSV";
            }
        }
        public override void Export(string fileName)
        {
            StringBuilder sb = new StringBuilder();
            if (Data.Count > 0)
            {
                Data.ForEach(x => sb.AppendLine(string.Join(",", new string[] { x.Name, x.CellNumber, x.EmailAddress })));
            }

            File.WriteAllText(Path.Combine(Folder, fileName + Extention), sb.ToString());

        }
        public override void Import(string fileName)
        {
            Data.Clear();

            Data.AddRange((from line in File.ReadAllLines(Path.Combine(Folder, fileName + Extention))
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
