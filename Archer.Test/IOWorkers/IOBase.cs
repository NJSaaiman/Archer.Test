using Archer.Test.DTO;
using Archer.Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archer.Test.IOWorkers
{
    public abstract class IOBase : IIOBase
    {
        private List<ClientDataDTO> _data;
        public List<ClientDataDTO> Data { get { return _data; } }

        protected abstract string Extention { get; }
        public abstract string Type { get; }
        public IOBase(List<ClientDataDTO> data)
        {
            _data = data;
        }

        public IOBase()
            : this(new List<ClientDataDTO>()) { }

        public bool AddRecord(ClientDataDTO data)
        {
            _data.Add(data);
            return true;
        }

        public abstract void Export(string fileName);
        public abstract void Import(string fileName);
    }
}
