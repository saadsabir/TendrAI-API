using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TendrAI.Application.Ports.In;
using TendrAI.Application.Ports.Out;
using TendrAI.Application.UseCases;
using TendrAI.Infrastructure.Adapters.ChatGpt;
using TendrAI.Infrastructure.Adapters.Pdf;

var builder = WebApplication.CreateBuilder(args);

// Configuration of settings (appsettings.json)
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Register services (Dependency Injection)
builder.Services.AddScoped<IPdfPigTextExtractorService, PdfPigTextExtractorService>();
builder.Services.AddScoped<IAnalyserAppelOffre, AnalyserAppelOffreUseCase>();
builder.Services.AddScoped<IChatClient>(provider =>
    new OpenAiChatClient(builder.Configuration["OpenAI:ApiKey"]));
builder.Services.AddScoped<IOpenAiAssistantService, OpenAiAssistantService>();

builder.Services.AddHttpClient<IOpenAiAssistantService, OpenAiAssistantService>(client =>
{
    client.BaseAddress = new Uri("https://api.openai.com/v1/");
    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {builder.Configuration["OpenAI:ApiKey"]}");
});

// Add controllers (for REST API)
builder.Services.AddControllers();

// Swagger (development only)
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();

// HTTP middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();