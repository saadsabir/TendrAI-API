using OpenAI.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TendrAI.Application.Ports.Out;

namespace TendrAI.Infrastructure.Adapters.ChatGpt
{
    public class OpenAiChatClient : IChatClient
    {
        private readonly string _apiKey;

        public OpenAiChatClient(string apiKey)
        {
            _apiKey = apiKey;
        }

        public string GetCompletion(List<ChatMessage> messages)
        {
            ChatClient client = new(
                model: "gpt-4.1",
                apiKey: _apiKey
            );

            ChatCompletion completion = client.CompleteChat(messages);

            return completion.Content[0].Text;
        }
    }
}
