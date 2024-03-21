using Newtonsoft.Json;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Tunify.Helpers
{
    internal class SpotifyHelper
    {
        static async Task GetToken(string[] args)
        {
            string configPath = "config.json";
            Config config = LoadConfig(configPath);

            var authOptions = new
            {
                url = "https://accounts.spotify.com/api/token",
                headers = new
                {
                    Authorization = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(config.clientId + ":" + config.clientSecret))
                },
                form = new
                {
                    grant_type = "client_credentials"
                }
            };

            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(authOptions), Encoding.UTF8, "application/json");

                var response = await client.PostAsync(authOptions.url, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic tokenResponse = JsonConvert.DeserializeObject(responseBody);
                    string token = tokenResponse.access_token;
                    Console.WriteLine("Token: " + token);

                    // Mettre à jour le token dans le fichier de configuration
                    config.token = token;
                    SaveConfig(config, configPath);
                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode);
                }
            }
        }

        static Config LoadConfig(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"Le fichier de configuration '{path}' n'a pas été trouvé.");
            }

            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Config>(json);
        }

        static void SaveConfig(Config config, string path)
        {
            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(path, json);
        }
    }

    class Config
    {
        public string clientId { get; set; }
        public string clientSecret { get; set; }
        public string token { get; set; }
    }
}
