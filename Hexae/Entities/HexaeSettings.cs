using System.IO;
using System.Threading.Tasks;
using Hexae.Entities.Settings;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Hexae.Entities
{
    /// <summary>
    /// Representa as configurações do bot.
    /// </summary>
    public class HexaeSettings
    {
        /// <summary>
        /// Determina as configurações do cliente do discord.
        /// </summary>
        [JsonProperty]
        public DiscordSettings Discord { get; private set; } = new DiscordSettings();

        /// <summary>
        /// Determina as configurações do módulo interactivity.
        /// </summary>
        [JsonProperty]
        public InteractivitySettings Interactivity { get; private set; } = new InteractivitySettings();

        /// <summary>
        /// Inicializa a configuração da skylar.
        /// </summary>
        /// <returns>Instância da configuração importada do arquivo ou criada do zero.</returns>
        public static async Task<HexaeSettings> InitializeAsync()
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };

            var config = new HexaeSettings();
            var file = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), "Config.json"));

            string json;

            if (!file.Exists)
            {
                json = JsonConvert.SerializeObject(config, Formatting.Indented, settings);
                using var sw = file.CreateText();
                await sw.WriteLineAsync(json);
                await sw.FlushAsync();
            }
            else
            {
                using var sr = file.OpenText();
                json = await sr.ReadToEndAsync();
                config = JsonConvert.DeserializeObject<HexaeSettings>(json, settings);
            }

            return config;
        }
    }
}
