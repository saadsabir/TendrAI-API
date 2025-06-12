using System.Net.Http.Headers;
using TendrAI.Application.Ports.Out;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TendrAI.Infrastructure.Adapters;

public class MindeeOcrService : IOcrService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey = "TA_CLE_PRIVEE";

    public MindeeOcrService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> ExtractTextAsync(Stream pdfStream)
    {
        using var content = new MultipartFormDataContent();
        content.Add(new StreamContent(pdfStream), "document", "appel.pdf");

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Token", _apiKey);

        var response = await _httpClient.PostAsync("https://api.mindee.net/v1/products/mindee/invoice/v1/predict", content);

        response.EnsureSuccessStatusCode();
        var resultJson = await response.Content.ReadAsStringAsync();

        // 🔁 Extraire uniquement le texte utile du JSON Mindee
        var text = JObject.Parse(resultJson)["document"]?["inference"]?["ocr_output"]?.ToString();
        return text ?? string.Empty;
    }
}
