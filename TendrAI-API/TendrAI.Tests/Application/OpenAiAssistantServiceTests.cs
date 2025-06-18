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
using OpenAI.Chat;
using System.ClientModel;
using System.Data;
using Microsoft.Extensions.Configuration;
using TendrAI.Application.Ports.Out;

namespace TendrAI.Tests.Application
{
    public class OpenAiAssistantServiceTests
    {
        private OpenAiAssistantService _service = null!;
        private Mock<IChatClient> _mockChatClient = null!;
        private HttpClient _httpClient = null!;
        private IConfiguration _configuration = null!;

        public OpenAiAssistantServiceTests()
        {
            // Setup runs before each test
            _mockChatClient = new Mock<IChatClient>();

            _httpClient = new HttpClient(); // not used directly
            _configuration = new ConfigurationBuilder().AddInMemoryCollection().Build(); // dummy config

            _service = new OpenAiAssistantService(_httpClient, _configuration, _mockChatClient.Object);
        }

        [Fact]
        public void ResumerAppelOffre_ReturnsParsedAppelOffre()
        {
            // Arrange
            var fakeResponse = """
            Titre : Fourniture d’équipements
            Description : Achat de mobilier pour les bureaux.
            Date limite : 2025-09-01
            """;

            _mockChatClient.Setup(x => x.GetCompletion(It.IsAny<List<ChatMessage>>()))
                          .Returns(fakeResponse);

            var service = new OpenAiAssistantService(_httpClient, _configuration, _mockChatClient.Object);

            // Act
            var result = service.ResumerAppelOffre("Fake appel d'offre text");

            // Assert
            Assert.Equal("Fourniture d’équipements", result.Titre);
            Assert.Equal("Achat de mobilier pour les bureaux.", result.Description);
            Assert.Equal(new DateTime(2025, 9, 1), result.DateLimite);
        }

        [Fact]
        public void ParseAppelOffre_Should_ParseStructuredTextCorrectly()
        {
            // Arrange
            var rawResponse = """
            Titre : Fourniture de matériel informatique
            Description : Achat d’ordinateurs pour les écoles.
            Date limite : 2025-11-30
            """;

            var service = new OpenAiAssistantService(_httpClient, _configuration, _mockChatClient.Object);

            // Act
            var result = service.ParseAppelOffre(rawResponse);

            // Assert
            Assert.Equal("Fourniture de matériel informatique", result.Titre);
            Assert.Equal("Achat d’ordinateurs pour les écoles.", result.Description);
            Assert.Equal(new DateTime(2025, 11, 30), result.DateLimite);
        }

        [Fact]
        public void ParseAppelOffre_Should_HandleMissingFieldsGracefully()
        {
            // Arrange
            var rawResponse = """
            Titre : Marché de nettoyage
            Date limite : 2025-08-01
            """;

            var service = new OpenAiAssistantService(_httpClient, _configuration, _mockChatClient.Object);

            // Act
            var result = service.ParseAppelOffre(rawResponse);

            // Assert
            Assert.Equal("Marché de nettoyage", result.Titre);
            Assert.Null(result.Description);
            Assert.Equal(new DateTime(2025, 8, 1), result.DateLimite);
        }

        [Fact]
        public void ParseAppelOffre_Should_Return_MinDate_IfDateInvalid()
        {
            // Arrange
            var rawResponse = """
            Titre : Fourniture
            Description : Test
            Date limite : not-a-date
            """;

            var service = new OpenAiAssistantService(_httpClient, _configuration, _mockChatClient.Object);

            // Act
            var result = service.ParseAppelOffre(rawResponse);

            // Assert
            Assert.Equal(DateTime.MinValue, result.DateLimite);
        }
    }
}
