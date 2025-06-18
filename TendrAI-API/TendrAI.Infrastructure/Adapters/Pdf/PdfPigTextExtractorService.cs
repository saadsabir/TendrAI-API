using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TendrAI.Application.Ports.Out;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace TendrAI.Infrastructure.Adapters.Pdf
{
    public class PdfPigTextExtractorService : IPdfPigTextExtractorService
    {
        public async Task<string> ExtractTextAsync(Stream pdfStream)
        {
            if (pdfStream == null || !pdfStream.CanRead)
                throw new ArgumentException("Le stream PDF est invalide ou illisible.");

            using var memoryStream = new MemoryStream();
            await pdfStream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var stringBuilder = new StringBuilder();

            using var document = PdfDocument.Open(memoryStream);
            foreach (Page page in document.GetPages())
            {
                stringBuilder.AppendLine(page.Text);
            }

            return stringBuilder.ToString();
        }
    }
}
