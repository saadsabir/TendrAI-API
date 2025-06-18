using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TendrAI.Application.Ports.Out;
using TendrAI.Application.UseCases;
using TendrAI.Domain;
using TendrAI.Domain.Models;
using Xunit;

namespace TendrAI.Tests.Application
{
    public class AnalyserAppelOffreUseCaseTests
    {
        [Fact]
        public async Task ExecuteAsync_ReturnsExpectedAppelOffre()
        {
            // Arrange
            var mockExtractor = new Mock<IPdfPigTextExtractorService>();
            var mockIA = new Mock<IOpenAiAssistantService>();

            var fakePdfStream = new MemoryStream(Encoding.UTF8.GetBytes("Fake PDF content"));
            var extractedText = "Extracted text from PDF";
            var expectedAppel = new AppelOffre
            {
                Titre = "Test Title",
                Description = "Test Description",
                DateLimite = new DateTime(2025, 12, 31)
            };

            mockExtractor.Setup(x => x.ExtractTextAsync(It.IsAny<Stream>()))
                         .ReturnsAsync(extractedText);

            mockIA.Setup(x => x.ResumerAppelOffre(extractedText))
                  .Returns(expectedAppel);

            var useCase = new AnalyserAppelOffreUseCase(mockExtractor.Object, mockIA.Object);

            // Act
            var result = await useCase.ExecuteAsync(fakePdfStream);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedAppel.Titre, result.Titre);
            Assert.Equal(expectedAppel.Description, result.Description);
            Assert.Equal(expectedAppel.DateLimite, result.DateLimite);

            mockExtractor.Verify(x => x.ExtractTextAsync(It.IsAny<Stream>()), Times.Once);
            mockIA.Verify(x => x.ResumerAppelOffre(extractedText), Times.Once);
        }
    }
}
