using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TendrAI.Domain;

namespace TendrAI.Application.Ports.In
{
    public interface IAnalyserAppelOffre
    {
        Task<AppelOffre> ExecuteAsync(Stream pdfDocument);
    }
}
