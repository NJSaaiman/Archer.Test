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
        public IOToJson(List<ClientDataDTO> data) : base(data) { }
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
        public override void Export(string fileName)
        {
            File.WriteAllText(Path.Combine(Folder, fileName + Extention), JsonConvert.SerializeObject(Data));
        }
        public override void Import(string fileName)
        {
            Data.Clear();

            Data.AddRange(JsonConvert.DeserializeObject<List<ClientDataDTO>>(File.ReadAllText(Path.Combine(Folder, fileName + Extention))));
            
        }
    }
}
