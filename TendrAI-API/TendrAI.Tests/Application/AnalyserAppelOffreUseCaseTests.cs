using Xunit;
using Moq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TendrAI.Application.Ports.In;
using TendrAI.Application.Ports.Out;
using TendrAI.Application.UseCases;
using TendrAI.Domain;

namespace TendrAI.Tests.Application
{
    public class AnalyserAppelOffreUseCaseTests
    {
        [Fact]
        public async Task ExecuteAsync_ReturnsExpectedAppelOffre()
        {
            // Arrange
            var fakePdf = new MemoryStream(Encoding.UTF8.GetBytes("PDF content"));

            var mockOcr = new Mock<IOcrService>();
            mockOcr.Setup(x => x.ExtractTextAsync(It.IsAny<Stream>()))
                   .ReturnsAsync("texte OCR extrait");

            var expectedAppel = new AppelOffre
            {
                Id = Guid.NewGuid(),
                Titre = "Réhabilitation école",
                Description = "Travaux bâtiment",
                DateLimite = DateTime.Today.AddDays(15)
            };

            var mockIA = new Mock<IAssistantIAService>();
            mockIA.Setup(x => x.ResumerAppelOffreAsync("texte OCR extrait"))
                  .ReturnsAsync(expectedAppel);

            var useCase = new AnalyserAppelOffreUseCase(mockOcr.Object, mockIA.Object);

            // Act
            var result = await useCase.ExecuteAsync(fakePdf);

            // Assert
            Assert.Equal(expectedAppel.Titre, result.Titre);
            Assert.Equal(expectedAppel.Description, result.Description);
            Assert.Equal(expectedAppel.DateLimite, result.DateLimite);
        }
    }
}
