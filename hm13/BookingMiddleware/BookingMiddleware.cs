namespace hm13.BookingMiddleware
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using System.Threading.Tasks;

    public class BookingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public BookingMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var bookingNotAllowed = _configuration.GetValue<bool>("BookingNotAllowed");
            if (bookingNotAllowed)
            {
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync("Booking is not allowed at this time.");
                return;
            }

            await _next(context);
        }
    }

}
