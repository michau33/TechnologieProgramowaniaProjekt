using System;
using System.Collections.Generic;
using System.Text;

namespace TpProjektModel.Interfaces
{
    public interface IWriter
    {
        void Write<T>(T obj, string filePath);
    }
}
