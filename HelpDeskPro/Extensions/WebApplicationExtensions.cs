using HelpDeskPro.Middleware;
using Serilog;

namespace HelpDeskPro.Extensions
{
    /// <summary>
    /// Extensiones para configurar el pipeline de la aplicación.
    /// </summary>
    public static class WebApplicationExtensions
    {
        /// <summary>
        /// Configura el pipeline de HelpDeskPro con Swagger, localización, logging, seguridad y controladores.
        /// </summary>
        public static WebApplication UseHelpDeskProPipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRequestLocalization();

            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseApiExceptionMiddleware();
            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
