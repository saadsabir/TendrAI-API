using Moq.Protected;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TendrAI.Infrastructure.Adapters;
using Xunit;

namespace TendrAI.Tests.Infrastructure
{
    public class MindeeOcrServiceTests
    {
        [Fact]
        public async Task ExtractTextAsync_ReturnsText_WhenMindeeReturnsValidResponse()
        {
            var json = @"{ 'document': { 'inference': { 'ocr_output': 'Texte extrait' } } }";

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                   .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                   .ReturnsAsync(new HttpResponseMessage
                   {
                       StatusCode = HttpStatusCode.OK,
                       Content = new StringContent(json)
                   });

            var client = new HttpClient(handler.Object);
            var service = new MindeeOcrService(client);

            var result = await service.ExtractTextAsync(new MemoryStream());

            Assert.Equal("Texte extrait", result);
        }
    }
}
