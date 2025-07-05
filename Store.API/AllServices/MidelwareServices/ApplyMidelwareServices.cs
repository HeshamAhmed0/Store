using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Store.API.Middelware;

namespace Store.API.Extentions.MidelwareServices
{
    public static class ApplyMidelwareServices
    {
        public static WebApplication ApplyMidelware( this WebApplication app)
        {

            app.UseMiddleware<GlobalHandleErroeMiddleware>();

            return app;
        }
    }
}
