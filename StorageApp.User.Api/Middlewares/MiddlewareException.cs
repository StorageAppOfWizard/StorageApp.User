namespace StorageApp.User.Api.Middlewares
{
    public class MiddlewareException
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MiddlewareException> _logger;

        public MiddlewareException(RequestDelegate next, ILogger<MiddlewareException> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Uhandle Exception");
                context.Response.StatusCode = 500;

                await context.Response.WriteAsJsonAsync(new
                {
                    error = "Something went wrong"
                });
            }
        }
    }
}
