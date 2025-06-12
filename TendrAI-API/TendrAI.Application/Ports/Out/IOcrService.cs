using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TendrAI.Application.Ports.Out
{
    public interface IOcrService
    {
        Task<string> ExtractTextAsync(Stream pdfStream);
    }
}
