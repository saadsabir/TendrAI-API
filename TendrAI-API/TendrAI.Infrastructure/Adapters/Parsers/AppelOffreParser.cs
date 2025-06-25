using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TendrAI.Application.Ports.Out;
using TendrAI.Domain.Models;

namespace TendrAI.Infrastructure.Adapters.Parsers
{
    public class AppelOffreParser : IAppelOffreParser
    {
        public AppelOffre ParseAppelOffre(string message)
        {
            var appel = new AppelOffre();
            var lines = message.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                if (line.Contains("Titre", StringComparison.OrdinalIgnoreCase))
                    appel.Titre = line.Split(':', 2)[1].Trim();
                else if (line.Contains("Description", StringComparison.OrdinalIgnoreCase))
                    appel.Description = line.Split(':', 2)[1].Trim();
                else if (line.Contains("Date limite", StringComparison.OrdinalIgnoreCase))
                {
                    appel.DateLimite = line.Split(':', 2)[1].Trim();
                }
            }

            return appel;
        }
    }
}
