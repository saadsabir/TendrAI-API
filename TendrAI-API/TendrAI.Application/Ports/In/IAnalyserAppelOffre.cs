using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TendrAI.Domain.Models;

namespace TendrAI.Application.Ports.In
{
    /// <summary>
    /// Asynchronously processes a PDF document stream by extracting its text content and summarizing it into an AppelOffre object.
    /// </summary>
    /// <param name="pdfDocument">The input PDF document as a stream.</param>
    /// <returns>An AppelOffre object representing the summarized information extracted from the PDF.</returns>
    public interface IAnalyserAppelOffre
    {
        Task<AppelOffre> ExecuteAsync(Stream pdfDocument);
    }
}
