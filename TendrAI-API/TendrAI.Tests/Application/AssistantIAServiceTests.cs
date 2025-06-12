using Xunit;
using Moq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using TendrAI.Infrastructure.Adapters;
using TendrAI.Domain;
using Moq.Protected;

namespace TendrAI.Tests.Application
{
    public class AssistantIAServiceTests
    {
        [Fact]
        public async Task ResumerAppelOffreAsync_ParsesOpenAIResponse()
        {
            // Arrange : réponse JSON mockée
            var responseContent = @"{
            'choices': [{
                'message': {
                    'content': 'Titre : Marché public école\nDescription : Construction bâtiment\nDate limite : 2025-07-15'
                }
            }]
        }";

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                   .Setup<Task<HttpResponseMessage>>("SendAsync",
                       ItExpr.IsAny<HttpRequestMessage>(),
                       ItExpr.IsAny<CancellationToken>())
                   .ReturnsAsync(new HttpResponseMessage
                   {
                       StatusCode = HttpStatusCode.OK,
                       Content = new StringContent(responseContent, Encoding.UTF8, "application/json")
                   });

            var httpClient = new HttpClient(handler.Object);

            var service = new OpenAiAssistantService(httpClient);
            var result = await service.ResumerAppelOffreAsync("Texte quelconque");

            // Assert
            Assert.Equal("Marché public école", result.Titre);
            Assert.Equal("Construction bâtiment", result.Description);
            Assert.Equal(new DateTime(2025, 07, 15), result.DateLimite);
        }
    }
}
