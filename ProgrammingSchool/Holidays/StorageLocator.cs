using NDatabase.Api;

namespace Holidays
{
    public class StorageLocator
    {
        private static IOdb storage;

        public static IOdb Get()
        {
            return storage;
        }

        public static void Set(IOdb storage)
        {
            StorageLocator.storage = storage;
        }
    }
}
