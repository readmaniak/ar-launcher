namespace ARLauncher
{
    internal static class Program
    {
        private const string JsonConfig = "AtlasReactorConfig.json";

        public static string JsonConfigFullPath => Path.Combine(Environment.CurrentDirectory, JsonConfig);
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
            SteamConnector.Shutdown();
        }
    }
}