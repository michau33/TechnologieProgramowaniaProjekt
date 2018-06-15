using System;
using System.Collections.Generic;
using System.Text;

namespace TpProjektModel.Interfaces
{
    public interface IReader
    {
        T Read<T>(string filePath);
    }
}
