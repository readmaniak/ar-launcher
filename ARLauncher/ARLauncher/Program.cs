using ARLauncher;

var ticket = await SteamConnector.GetSteamTicket();

SteamConnector.Shutdown();
