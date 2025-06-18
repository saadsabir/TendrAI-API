using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using TendrAI.Application.Ports.Out;
using System.Text;
using OpenAI.Chat;
using TendrAI.Domain.Models;
using TendrAI.Infrastructure.Adapters.Parsers;

public class OpenAiAssistantService : IOpenAiAssistantService
{
    private readonly IChatClient _chatClient;
    private readonly IAppelOffreParser _parser;

    public OpenAiAssistantService(IConfiguration config, IChatClient chatClient, IAppelOffreParser parser)
    {
        _chatClient = chatClient ?? throw new ArgumentNullException(nameof(chatClient));
        _parser = parser;
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

        try
        {
            var response = _chatClient.GetCompletion(messages);

            if (string.IsNullOrWhiteSpace(response))
                throw new InvalidOperationException("La réponse de l'IA est vide.");

            return _parser.ParseAppelOffre(response);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Une erreur est survenue lors de la génération du résumé par OpenAI.", ex);
        }
    }
}