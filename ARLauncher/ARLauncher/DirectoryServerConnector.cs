using EvoS.DirectoryServer.ARLauncher.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace ARLauncher
{
    internal class DirectoryServerConnector
    {
        private static readonly Lazy<DirectoryServerConnector> _instance = new(() => new());
        public static DirectoryServerConnector Instance => _instance.Value;

        private readonly string _address;
        private readonly HttpClient _client;

        private DirectoryServerConnector()
        {
            var address = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(Program.JsonConfigFullPath))?["DirectoryServerAddress"]?.Value<string>();
            if (address == null)
            {
                throw new Exception($"Config file ({Program.JsonConfigFullPath}) not found, or contains incorrect text. Check that ARLauncher is located in Config folder.");
            }
            _address = address.StartsWith("http://") ? address : $"http://{address}";
            _client = new HttpClient();
        }

        public async Task<T> GetAsync<T>(LauncherRequest data, CancellationToken cancellationToken) where T : LauncherResponseBase
        {
            var text = JsonConvert.SerializeObject(data);
            var resp = await _client.PostAsync(_address, new StringContent(text), cancellationToken);
            resp.EnsureSuccessStatusCode();
            var str = await resp.Content.ReadAsStringAsync(cancellationToken);
            var res = JsonConvert.DeserializeObject<T>(str);
            if (res == null)
                throw new Exception("unexpected error: could not deserialize response from server");
            return res;
        }
    }
}
