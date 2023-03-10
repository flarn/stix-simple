namespace Stix.Core
{
    public static class IdGenerator
    {
        public static string Generate<T>()
        {
            return $"{typeof(T).Name}--{Guid.NewGuid()}".ToLowerInvariant();
        }
    }
}