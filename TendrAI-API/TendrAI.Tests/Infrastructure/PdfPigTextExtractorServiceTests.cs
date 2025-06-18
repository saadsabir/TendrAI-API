using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TendrAI.Infrastructure.Adapters.Pdf;
using Xunit;

namespace TendrAI.Tests.Infrastructure
{
    public class PdfPigTextExtractorServiceTests
    {
        private readonly PdfPigTextExtractorService _service = new();

        [Fact]
        public async Task ExtractTextAsync_WithNullStream_ThrowsArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _service.ExtractTextAsync(null!));
        }

        [Fact]
        public async Task ExtractTextAsync_WithUnreadableStream_ThrowsArgumentException()
        {
            var unreadable = new UnreadableStream();
            await Assert.ThrowsAsync<ArgumentException>(() => _service.ExtractTextAsync(unreadable));
        }

        [Fact]
        public async Task ExtractTextAsync_WithRealPdf_ReturnsSomeText()
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, "Utils", "sample.pdf");

            if (!File.Exists(filePath))
            {
                // Skip test if file missing
                return;
            }

            using var stream = File.OpenRead(filePath);
            var text = await _service.ExtractTextAsync(stream);

            Assert.False(string.IsNullOrWhiteSpace(text));
            Assert.Contains("fake pdf", text, StringComparison.OrdinalIgnoreCase);
        }

        // Dummy unreadable stream for testing
        private class UnreadableStream : Stream
        {
            public override bool CanRead => false;
            public override bool CanSeek => false;
            public override bool CanWrite => false;
            public override long Length => throw new NotSupportedException();
            public override long Position { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }
            public override void Flush() => throw new NotSupportedException();
            public override int Read(byte[] buffer, int offset, int count) => throw new NotSupportedException();
            public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
            public override void SetLength(long value) => throw new NotSupportedException();
            public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();
        }
    }
}
