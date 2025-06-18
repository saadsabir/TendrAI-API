using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TendrAI.Domain.Models;

namespace TendrAI.Application.Ports.Out
{
    public interface IOpenAiAssistantService
    {
        /// <summary>
        /// Generates a structured summary of a public procurement notice (appel d'offres) from a given text input using a GPT-based AI model.
        /// </summary>
        /// <param name="texte">The raw text of the appel d'offres to be summarized.</param>
        /// <returns>
        /// An <see cref="AppelOffre"/> object containing extracted details such as title, description, and deadline date.
        /// </returns>
        AppelOffre ResumerAppelOffre(string texte);
    }
}
