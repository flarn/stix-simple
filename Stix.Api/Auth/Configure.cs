using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Stix.Api.Auth
{
    public static class Configure
    {

        public static WebApplicationBuilder AddIdentity(this WebApplicationBuilder builder)
        {

            builder.Services.AddDbContext<AuthDdbContext>(o => o.UseSqlServer(builder.Configuration["IdentitySqlConnectionString"]));


            builder.Services
                .AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<AuthDdbContext>()
                .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options => { });

            builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ClockSkew = TimeSpan.Zero,
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["JwtIssuer"],
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["JwtAudiance"],

                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSigningKey"]!)),
                    };
                });

            builder.Services.AddAuthorization();

            return builder;
        }
    }
}
