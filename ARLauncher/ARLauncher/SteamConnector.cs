using Steamworks;
using System.Text;

namespace ARLauncher
{
    internal static class SteamConnector
    {
        public static void Init() => SteamClient.Init(480);
        
        public static async Task<string> GetSteamTicketAsync()
        {
            var ticket = await SteamUser.GetAuthSessionTicketAsync();
            if (ticket == null)
            {
                throw new Exception("Failed to get SessionTicket");
            }
            var array = ticket.Data;
            var sb = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                sb.Append(string.Format("{0:X2}", array[i]));
            }
            return sb.ToString();
        }

        public static void Shutdown() => SteamClient.Shutdown();
    }
}
