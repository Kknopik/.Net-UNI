namespace MarketSimulation.Logging
{
    public static class LogService
    {
        private static readonly HashSet<string> LoggedMessages = new HashSet<string>();

        public static void Log(string message)
        {
            if (!LoggedMessages.Contains(message))
            {
                Console.WriteLine(message);
                LoggedMessages.Add(message);
            }
        }

        public static void ClearLog()
        {
            LoggedMessages.Clear();
        }

        public static List<string> GetLogMessages()
        {
            return new List<string>(LoggedMessages);
        }
    }
}
