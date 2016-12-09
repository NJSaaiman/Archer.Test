using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archer.Test.DTO;
using System.IO;
using Newtonsoft.Json;

namespace Archer.Test.IOWorkers
{
    class IOClientB : IOBase
    {
        public IOClientB(List<ClientDataDTO> data) : base(data)
        {
        }

        public IOClientB() : base() { }

        public override void Export(string fileName)
        {
          File.WriteAllText(fileName+".json", JsonConvert.SerializeObject(Data));
        }

        public override void Import(string fileName)
        {
            Data.Clear();
            Data.AddRange(JsonConvert.DeserializeObject<List<ClientDataDTO>>(File.ReadAllText(fileName)));
        }
    }
}
