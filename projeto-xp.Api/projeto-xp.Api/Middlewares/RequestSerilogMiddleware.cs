using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace projeto_xp.Api.Middlewares
{
    public class RequestSerilogMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public RequestSerilogMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public Task Invoke(HttpContext context)
        {
            using (LogContext.PushProperty("UserName", context?.User?.Identity?.Name ?? "anonymous"))
            {
                return _requestDelegate.Invoke(context);
            }
        }
    }
}
