﻿using System.Collections.Generic;
using Archer.Test.DTO;

namespace Archer.Test.IOWorkers
{
    public interface IIOBase
    {
        List<ClientDataDTO> Data { get; }

        bool AddRecord(ClientDataDTO data);
        void Export(string fileName);
        void Import(string fileName);
    }
}