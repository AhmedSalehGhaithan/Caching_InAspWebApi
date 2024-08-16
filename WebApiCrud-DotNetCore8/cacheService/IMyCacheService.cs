namespace WebApiCrud_DotNetCore8.Services
{
    public interface IMyCacheService
    {
        T GetData<T>(string key);
        bool SetData<T>(string key,T value,DateTimeOffset expirationDate);
        object RemoveData(string key);
    }
}
