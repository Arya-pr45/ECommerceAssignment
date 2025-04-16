namespace ECommerce.Utilities
{
    public class DatabaseConnection
    {
        private static DatabaseConnection _instance;
        private static readonly object _lock = new();

        private DatabaseConnection() { }

        public static DatabaseConnection Instance
        {
            get
            {
                lock (_lock)
                {
                    return _instance ??= new DatabaseConnection();
                }
            }
        }

        public void Connect()
        {
            Console.WriteLine("Connected to DB!");
        }
    }
}