using HelpDeskPro.Middleware;
using Serilog;

namespace HelpDeskPro.Extensions
{
    public static class WebApplicationExtensions
    {
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
