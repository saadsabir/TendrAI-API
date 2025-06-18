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
using TendrAI.Domain.Models;

namespace TendrAI.Tests.Infrastructure
{
    public class OpenAiAssistantServiceTests
    {
        private OpenAiAssistantService _service = null!;
        private Mock<IChatClient> _mockChatClient = null!;
        private Mock<IAppelOffreParser>_parser = null!;
        private IConfiguration _configuration = null!;

        public OpenAiAssistantServiceTests()
        {
            // Setup runs before each test
            _mockChatClient = new Mock<IChatClient>();
            _parser = new Mock<IAppelOffreParser>();
            _configuration = new ConfigurationBuilder().AddInMemoryCollection().Build(); // dummy config

            _service = new OpenAiAssistantService(_configuration, _mockChatClient.Object, _parser.Object);
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

            var parsedAppel = new AppelOffre
            {
                Titre = "Fourniture d’équipements",
                Description = "Achat de mobilier pour les bureaux.",
                DateLimite = "Lundi 1 Septembre 2025"
            };

            _mockChatClient.Setup(x => x.GetCompletion(It.IsAny<List<ChatMessage>>()))
                          .Returns(fakeResponse);
            _parser.Setup(x => x.ParseAppelOffre(It.IsAny<string>())).Returns(parsedAppel);

            var service = new OpenAiAssistantService(_configuration, _mockChatClient.Object, _parser.Object);

            // Act
            var result = service.ResumerAppelOffre("Fake appel d'offre text");

            // Assert
            Assert.Equal("Fourniture d’équipements", result.Titre);
            Assert.Equal("Achat de mobilier pour les bureaux.", result.Description);
            Assert.Equal("Lundi 1 Septembre 2025", result.DateLimite);
        }


    }
}
