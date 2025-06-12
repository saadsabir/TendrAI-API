using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TendrAI.Application.Ports.Out;
using TendrAI.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TendrAI.Infrastructure.Adapters
{
    public class OpenAiAssistantService : IAssistantIAService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "sk-XXXX";

        public OpenAiAssistantService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AppelOffre> ResumerAppelOffreAsync(string texte)
        {
            var prompt = $"Voici le texte d'un appel d'offres :\n{texte}\n" +
                         "Résumé : Donne-moi un résumé structuré avec : titre, description, date limite, secteur si possible.";

            var body = new
            {
                model = "gpt-4",
                messages = new[]
                {
                new { role = "system", content = "Tu es un assistant expert en marchés publics." },
                new { role = "user", content = prompt }
            }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions")
            {
                Headers = { Authorization = new AuthenticationHeaderValue("Bearer", _apiKey) },
                Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();

            var message = JObject.Parse(json)["choices"]?[0]?["message"]?["content"]?.ToString();

            // 🧠 Extraire les infos du résumé retourné
            var lines = message?.Split('\n');
            var appel = new AppelOffre();

            foreach (var line in lines ?? Array.Empty<string>())
            {
                if (line.StartsWith("Titre")) appel.Titre = line.Split(':')[1].Trim();
                if (line.StartsWith("Description")) appel.Description = line.Split(':')[1].Trim();
                if (line.StartsWith("Date limite")) appel.DateLimite = DateTime.TryParse(line.Split(':')[1].Trim(), out var d) ? d : DateTime.MinValue;
            }

            return appel;
        }
    }
}
