using Archer.Test.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archer.Test.IOWorkers
{
    public static class IOFactory
    {
        public static IIOBase GetIOWorker(string name, List<ClientDataDTO> data)
        {
            Type type = Type.GetType(name, true);
            object newInstance = Activator.CreateInstance(type, data);
            return newInstance as IIOBase;
        }

        public static IIOBase GetIOWorker(string name)
        {
            Type type = Type.GetType(name, true);
            object newInstance = Activator.CreateInstance(type);
            return newInstance as IIOBase;
        }
    }
}
