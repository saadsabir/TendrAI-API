using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TendrAI.Domain.Models;

namespace TendrAI.Application.Ports.Out
{
    public interface IAppelOffreParser
    {
        /// <summary>
        /// Parses a structured tender summary from a given text response. 
        /// Extracts fields such as title, description, and deadline date.
        /// </summary>
        /// <param name="message">The input message containing the tender information, usually from an AI model response.</param>
        /// <returns>An <see cref="AppelOffre"/> object with parsed data.</returns>
        AppelOffre ParseAppelOffre(string message);
    }
}
