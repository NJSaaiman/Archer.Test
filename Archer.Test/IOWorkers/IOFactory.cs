using Archer.Test.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archer.Test.IOWorkers
{
    public sealed class IOFactory
    {
        private static readonly IOFactory _instance = new IOFactory();

        public static IOFactory Instance
        {
            get
            {
                return _instance;
            }
        }

        public IIOBase GetIOWorker(string name)
        {
            return GetIOWorker(name, null);
        }
        public IIOBase GetIOWorker(string name, List<ClientDataDTO> data)
        {
            Type type = Type.GetType(name, true);
            object newInstance = Activator.CreateInstance(type, data);
            return newInstance as IIOBase;
        }

       
    }
}
