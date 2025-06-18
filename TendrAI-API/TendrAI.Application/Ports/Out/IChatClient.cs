using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenAI.Chat;

namespace TendrAI.Application.Ports.Out
{
    public interface IChatClient
    {
        string GetCompletion(List<ChatMessage> messages);
    }
}
