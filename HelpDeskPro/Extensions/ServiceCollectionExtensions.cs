using FluentValidation;
using HelpDeskPro.Consts;
using HelpDeskPro.Data;
using HelpDeskPro.Data.Repositories.RefreshTokenRepository;
using HelpDeskPro.Data.Repositories.TicketRepository;
using HelpDeskPro.Data.Repositories.UserRepository;
using HelpDeskPro.Dtos.Auth.Validators;
using HelpDeskPro.Services;
using HelpDeskPro.Services.AuthService;
using HelpDeskPro.Services.TicketService;
using HelpDeskPro.Services.UserService;
using HelpDeskPro.Shared;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using System.Globalization;
using System.Reflection;

namespace HelpDeskPro.Extensions
{
    /// <summary>
    /// Extensiones para registrar servicios de HelpDeskPro (Swagger, validación, persistencia y localización).
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        // ---------- Swagger ----------
        /// <summary>
        /// Configura Swagger/OpenAPI con seguridad Bearer y comentarios XML.
        /// </summary>
        public static IServiceCollection AddHelpDeskProSwagger(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "HelpDeskPro API",
                    Version = "v1",
                    Description = "API para gestión de tickets de soporte"
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Inserta el token JWT con el prefijo 'Bearer '. Ejemplo: 'Bearer eyJhbGciOi...'.",
                });

                options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("Bearer", document)] = []
                });

                options.AddServer(new OpenApiServer
                {
                    Description = "Dev",
                    Url = "https://localhost:7198"
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
                if (File.Exists(xmlPath))
                {
                    options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
                }
            });

            return services;
        }

        // ---------- FluentValidation + filtros ----------
        /// <summary>
        /// Configura FluentValidation y filtro global de validación.
        /// </summary>
        public static IServiceCollection AddHelpDeskProValidation(this IServiceCollection services)
        {
            // Global FV config
            ValidatorOptions.Global.LanguageManager.Enabled = true;

            services.AddValidatorsFromAssemblyContaining<LoginRequestDtoValidator>();

            services.AddScoped<FluentValidationActionFilter>();

            services.AddControllers(options =>
            {
                options.Filters.Add<FluentValidationActionFilter>();
            });

            return services;
        }

        // ---------- Persistencia, repos y servicios de dominio ----------
        /// <summary>
        /// Configura DbContext, repositorios y servicios de dominio.
        /// </summary>
        public static IServiceCollection AddHelpDeskProPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<HelpDeskProContext>(dbContextOptions =>
                dbContextOptions.UseSqlite(
                    configuration["ConnectionStrings:HelpDeskProDBConnectionString"]));

            // Servicios de dominio
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITicketService, TicketService>();

            // Repositorios
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();


            // Infra común
            services.AddAutoMapper(cfg => { }, typeof(Program).Assembly);
            services.AddHttpContextAccessor();

            return services;
        }

        // ---------- Localización / cultura ----------
        /// <summary>
        /// Configura localización soportando códigos de idioma disponible y proveedor de cultura por claim.
        /// </summary>
        public static IServiceCollection AddHelpDeskProLocalization(this IServiceCollection services)
        {
            var supportedCultures = Languages.GetSupportedLanguageCodes()
                .Select(code => new CultureInfo(code.ToLowerInvariant()))
                .ToList();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("es");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;

                options.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new UserClaimRequestCultureProvider(),
                    new AcceptLanguageHeaderRequestCultureProvider()
                };
            });

            return services;
        }
    }
}
