using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TendrAI.Application.Ports.In;
using TendrAI.Application.Ports.Out;
using TendrAI.Domain.Models;

namespace TendrAI.Application.UseCases
{
    public class AnalyserAppelOffreUseCase : IAnalyserAppelOffre
    {
        private readonly IPdfPigTextExtractorService _extract;
        private readonly IOpenAiAssistantService _ia;


        public AnalyserAppelOffreUseCase(IPdfPigTextExtractorService extract, IOpenAiAssistantService ia)
        {
            _extract = extract;
            _ia = ia;
        }

        public async Task<AppelOffre> ExecuteAsync(Stream pdfDocument)
        {
            var text = await _extract.ExtractTextAsync(pdfDocument); //using PdfPig
            var appel = _ia.ResumerAppelOffre(text);
            return appel;
        }
    }
}
