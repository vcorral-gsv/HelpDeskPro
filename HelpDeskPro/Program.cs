using HelpDeskPro.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// ---------- Logging (Serilog) ----------
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.WithProperty("Application", "HelpDeskPro")
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog(Log.Logger, dispose: true);

// ---------- Routing ----------
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// ---------- Servicios de la app ----------
builder.Services
    .AddHelpDeskProSwagger(builder.Configuration)
    .AddHelpDeskProValidation()
    .AddHelpDeskProPersistence(builder.Configuration)
    .AddHelpDeskProAuth(builder.Configuration)
    .AddHelpDeskProLocalization();

var app = builder.Build();

// ---------- Pipeline ----------
app.UseHelpDeskProPipeline();

app.Run();
