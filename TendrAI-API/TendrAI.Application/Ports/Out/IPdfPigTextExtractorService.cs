using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TendrAI.Application.Ports.Out
{
    public interface IPdfPigTextExtractorService
    {
        /// <summary>
        /// Asynchronously extracts all text content from a PDF stream using PdfPig.
        /// </summary>
        /// <param name="pdfStream">A readable stream containing the PDF document.</param>
        /// <returns>
        /// A <see cref="string"/> containing the extracted text from all pages of the PDF.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the input stream is null or unreadable.</exception>
        Task<string> ExtractTextAsync(Stream pdfStream);
    }
}
