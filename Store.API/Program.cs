
using Domain.Contracts;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Reposatory;
using Services;
using Services_Absractions;
using Store.API.Middelware;

namespace Store.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IDbInitializer, DbInitializer>();
            builder.Services.AddScoped<IServiceManager,ServicesManager>();
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(AssemplyReference).Assembly);

            builder.Services.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = (actionContext) =>
                {
                  var ErrorResponse=  actionContext.ModelState.Where(m => m.Value.Errors.Any()).
                                            Select(m => new ValidationError()
                                            {
                                                Field=m.Key,
                                                Errors=m.Value.Errors.Select(e=>e.ErrorMessage).ToList(),

                                            }).ToList();

                    var response = new ValidationErrorResponse()
                    {
                        errors = ErrorResponse,
                    };
                    return new BadRequestObjectResult(response);
                };
            });

            var app = builder.Build();

            app.UseMiddleware<GlobalHandleErroeMiddleware>();

            #region DataSeeding
            var Scope =app.Services.CreateScope();
            var DbInitializer=Scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await DbInitializer.InitializeAsync();
            #endregion

            app.UseStaticFiles();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
