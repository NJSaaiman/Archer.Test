using Archer.Test.DTO;
using Archer.Test.Helpers;
using Archer.Test.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archer.Test.IOWorkers
{
    public abstract class IOBase : IIOBase
    {
        private List<ClientDataDTO> _data;
        public List<ClientDataDTO> Data { get { return _data; } }

        protected virtual string Folder
        {
            get
            {
                if (!Directory.Exists(Config.Instance.DropLocation))
                {
                    Directory.CreateDirectory(Config.Instance.DropLocation);
                }
                return Config.Instance.DropLocation;
            }
        }
        protected abstract string Extention { get; }
        public abstract string Type { get; }

        public IOBase(List<ClientDataDTO> data)
        {
            _data = data ?? new List<ClientDataDTO>();
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

        public virtual void DeleteFile(string fileName)
        {
            if (File.Exists(Path.Combine(Folder, fileName + Extention)))
            {
                File.Delete(Path.Combine(Folder, fileName + Extention));
            }
        }

    }
}

