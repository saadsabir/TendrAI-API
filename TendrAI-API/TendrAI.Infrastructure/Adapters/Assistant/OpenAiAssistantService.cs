using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using TendrAI.Application.Ports.Out;
using System.Text;
using OpenAI.Chat;
using TendrAI.Domain.Models;

public class OpenAiAssistantService : IOpenAiAssistantService
{
    private readonly HttpClient _httpClient;
    private readonly IChatClient _chatClient;

    public OpenAiAssistantService(HttpClient httpClient, IConfiguration config, IChatClient chatClient)
    {
        _httpClient = httpClient;
        _chatClient = chatClient ?? throw new ArgumentNullException(nameof(chatClient));
    }

    public AppelOffre ResumerAppelOffre(string texte)
    {
        var prompt = $"Voici le texte d'un appel d'offres :\n{texte}\n" +
                     "Résumé : Donne-moi un résumé structuré avec : titre, description, date limite, secteur si possible.";

        var messages = new List<ChatMessage>
        {
            new SystemChatMessage("Tu es un assistant expert en marchés publics."),
            new UserChatMessage(prompt)
        };

        var response = _chatClient.GetCompletion(messages);
        return ParseAppelOffre(response);
    }

    public AppelOffre ParseAppelOffre(string message)
    {
        var appel = new AppelOffre();
        var lines = message.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            if (line.StartsWith("Titre", StringComparison.OrdinalIgnoreCase))
                appel.Titre = line.Split(':', 2)[1].Trim();
            else if (line.StartsWith("Description", StringComparison.OrdinalIgnoreCase))
                appel.Description = line.Split(':', 2)[1].Trim();
            else if (line.StartsWith("Date limite", StringComparison.OrdinalIgnoreCase))
                appel.DateLimite = DateTime.TryParse(line.Split(':', 2)[1].Trim(), out var d) ? d : DateTime.MinValue;
        }

        return appel;
    }
}