using Microsoft.Extensions.Configuration;
using Mistral.SDK;
using Mistral.SDK.Models;
using TendrAI.Application.Ports.Out;
using TendrAI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mistral.SDK.DTOs;
using TendrAI.Infrastructure.Adapters.Parsers;

namespace TendrAI.Infrastructure.Adapters.Assistant
{
    public class MistralAssistantService : IMistralAssistantService
    {
        private readonly MistralClient _client;
        private readonly IAppelOffreParser _parser;

        public MistralAssistantService(IConfiguration config, IAppelOffreParser parser)
        {
            var apiKey = config["Mistral:ApiKey"]
                ?? throw new ArgumentNullException("Mistral:ApiKey");
            _client = new MistralClient(apiKey);
            _parser = parser;
        }

        public async Task<AppelOffre> ResumerAppelOffre(string texte)
        {
            var messages = new List<ChatMessage>
            {
                new ChatMessage(ChatMessage.RoleEnum.System,
                    "Tu es un assistant expert en marchés publics."),
                new ChatMessage(ChatMessage.RoleEnum.User,
                    $"Voici le texte d'un appel d'offres :\n{texte}\n. Extraire à partir de ce texte les éléments suivant : titre, description, date limite, nombre de lots, resumé court de chaque lots." +
                    $"La réponse doit respecter ce format : - **Titre** : - **Description** : - **Date Limite ** : - **Nombre de lots ** : - **Lots ** : - **Resumé du lot ** :")
            };

            var request = new ChatCompletionRequest(
                ModelDefinitions.MistralLarge,
                messages,
                temperature: (decimal?)0.2,
                maxTokens: 500
            );
            try
            {
                var response = await _client.Completions.GetCompletionAsync(request);
                var content = response.Choices[0].Message.Content;
                return _parser.ParseAppelOffre(content);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Une erreur est survenue lors de la génération du résumé par Mistral.", ex);
            }
        }

       
    }
}
