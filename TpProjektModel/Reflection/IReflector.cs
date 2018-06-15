using System.Collections.Generic;
using TpProjektModel.Reflection.Model;

namespace TpProjektModel.Reflection
{
    public interface IReflector
    {
        Dictionary<string, TypeModel> Beans { get; }
        AssemblyModel AssemblyModel { get; }
        void Reflect(string path);
    }
}