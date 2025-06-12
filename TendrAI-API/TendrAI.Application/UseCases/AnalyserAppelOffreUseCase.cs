using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TendrAI.Application.Ports.In;
using TendrAI.Application.Ports.Out;
using TendrAI.Domain;

namespace TendrAI.Application.UseCases
{
    public class AnalyserAppelOffreUseCase : IAnalyserAppelOffre
    {
        private readonly IOcrService _ocr;
        private readonly IAssistantIAService _ia;

        public AnalyserAppelOffreUseCase(IOcrService ocr, IAssistantIAService ia)
        {
            _ocr = ocr;
            _ia = ia;
        }

        public async Task<AppelOffre> ExecuteAsync(Stream pdfDocument)
        {
            var text = await _ocr.ExtractTextAsync(pdfDocument);
            var appel = await _ia.ResumerAppelOffreAsync(text);
            return appel;
        }
    }
}
