using Microsoft.Extensions.DependencyInjection.Extensions;
using Stix.Api.Filters;
using Stix.BLL;
using Stix.BLL.Interfaces;
using Stix.Core.Interfaces;
using Stix.Infrastructure;
using Stix.Api.Auth;

namespace Stix.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers(
                options =>
                {
                    options.Filters.Add<ExceptionFilter>();
                    options.Filters.Add<InputValidationFilter>();
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.TryAddScoped<IVulnerabilityService, VulnerabilityService>();

            builder.Services.AddScoped<IVulnerabilityRepository, VulnerabilityRepository>();

            builder.Services.AddEntityFrameworkCoreSqlServer(builder.Configuration["SqlConnectionString"]);

            builder.AddIdentity();


            var app = builder.Build();


            //Create vulnerability database
            using (var scope = app.Services.CreateScope())
                scope.ServiceProvider.GetRequiredService<StixDbContext>().Database.EnsureCreated();

            //Create users database
            using (var scope = app.Services.CreateScope())
                scope.ServiceProvider.GetRequiredService<AuthDdbContext>().Database.EnsureCreated();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}