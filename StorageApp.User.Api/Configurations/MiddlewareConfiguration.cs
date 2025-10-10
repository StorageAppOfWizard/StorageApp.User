using StorageApp.User.Api.Middlewares;

namespace StorageApp.User.Api.Configurations
{
    public static class MiddlewareConfiguration
    {
        public static IApplicationBuilder UseCustomMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<MiddlewareException>();
            return app;
        }
    }
}
