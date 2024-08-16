using System.Runtime.Caching;

namespace WebApiCrud_DotNetCore8.Services
{
    public class MyCacheService : IMyCacheService
    {
        private ObjectCache _memoryCache = MemoryCache.Default;
        //public MyCacheService(ObjectCache keyValuePairs)
        //{
        //    _memoryCache = keyValuePairs;
        //}
        public T GetData<T>(string key)
        {
            try
            {
                T item = (T)_memoryCache.Get(key);
                return item;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public object RemoveData(string key)
        {
            var res = true;

            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    var results = _memoryCache.Remove(key);
                }
                else
                    res = false;
                return res;
            }
            catch( Exception ex )
            {
                throw;
            }
        }

        public bool SetData<T>(string key, T value, DateTimeOffset expirationDate)
        {
            var result = true;
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    _memoryCache.Set(key, value, expirationDate);
                }
                else
                {
                    result = false;
                }

                return result;

            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
