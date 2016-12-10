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
    class IOToJson : IOBase
    {
        protected override string Extention
        {
            get
            {
                return ".json";
            }
        }

        public override string Type
        {
            get
            {
                return "JSON";
            }
        }

        public IOToJson(List<ClientDataDTO> data) : base(data)
        {
        }

        public IOToJson() : base() { }

        public override void Export(string fileName)
        {
            File.WriteAllText(fileName + Extention, JsonConvert.SerializeObject(Data));
        }

        public override void Import(string fileName)
        {
            Data.Clear();
            Data.AddRange(JsonConvert.DeserializeObject<List<ClientDataDTO>>(File.ReadAllText(fileName + Extention)));
            if (File.Exists(fileName + Extention))
            {
                File.Delete(fileName + Extention);
            }
        }
    }
}
