using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services_Absractions;

namespace Presentation.Attributes
{
    public class CachAttribute(int TimeForCache) : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var CachService =context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().CachService;

            var Key = GenerateKey(context.HttpContext.Request);
            var data = await CachService.GetAsync(Key);
            if(!string.IsNullOrEmpty(data))
            {
                context.Result = new ContentResult()
                {
                    ContentType = "application/json",
                    StatusCode=StatusCodes.Status200OK,
                    Content = data

                };
                return;
            }
            var ContentResult = await next.Invoke();
            if(ContentResult.Result is OkObjectResult okObject)
            {
               await  CachService.SetAsync(Key, okObject.Value, TimeSpan.FromSeconds(TimeForCache));
            }
        }

        private string GenerateKey(HttpRequest request)
        {
            var key =new StringBuilder();
            key.Append(request.Path);
            foreach(var item in request.Query.OrderBy(K=>K.Key))
            {
                key.Append($"{item.Key}-{item.Value}");
            }
            return key.ToString();
        }
    }
}
