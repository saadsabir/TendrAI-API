using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TendrAI.Domain.Models;

namespace TendrAI.Application.Ports.Out
{
    public interface IMistralAssistantService
    {
        /// <summary>
        /// Sends a tender notice text to the Mistral AI API and retrieves a structured summary.
        /// The summary includes the title, description, and deadline, which are then parsed into an AppelOffre object.
        /// </summary>
        /// <param name="texte">The full text of the tender notice to be summarized.</param>
        /// <returns>A task that returns an <see cref="AppelOffre"/> object with parsed tender data.</returns>
        /// <exception cref="ApplicationException">
        /// Thrown if an error occurs while requesting or processing the AI-generated response.
        /// </exception>
        Task<AppelOffre> ResumerAppelOffre(string texte);
    }
}
