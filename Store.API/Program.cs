
using Domain.Contracts;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Reposatory;
using Services;
using Services_Absractions;
using Store.API.Extentions.ApplyForAllServices;
using Store.API.Extentions.ApplyForAllServices_After_built;
using Store.API.Extentions.Infrustructure;
using Store.API.Extentions.MidelwareServices;
using Store.API.Extentions.Services;
using Store.API.Middelware;

namespace Store.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAllServices(builder.Configuration);

            var app = builder.Build();

            await app.ApplyAllServicesAsync();

            app.Run();
        }
    }
}
