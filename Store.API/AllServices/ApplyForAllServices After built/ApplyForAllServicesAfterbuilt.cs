using Domain.Contracts;
using Store.API.Extentions.MidelwareServices;
using Store.API.Middelware;

namespace Store.API.Extentions.ApplyForAllServices_After_built
{
    public static class ApplyForAllServicesAfterbuilt
    {
        public static async Task<WebApplication> ApplyAllServicesAsync(this WebApplication app)
        {
            app.ApplyMidelware();
            #region DataSeeding
            await InitializingDatabaze(app);
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

            return app;
        }

        private static async Task InitializingDatabaze(WebApplication app)
        {
            var Scope = app.Services.CreateScope();
            var DbInitializer = Scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await DbInitializer.InitializeAsync();
            await DbInitializer.InitializeIdentityAsync();
        }
    }
}
