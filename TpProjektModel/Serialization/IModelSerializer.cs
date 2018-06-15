namespace TpProjektModel.Serialization
{
    public interface IModelSerializer
    {
        void Write<T>(T obj, string filePath);
        T Read<T>(string filePath);
    }
}