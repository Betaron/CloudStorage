using System.Reflection;
using System.Text.Json.Serialization;
using CloudStorage.Core;
using CloudStorage.Data;
using CloudStorage.Web.HostedServices;
using CloudStorage.Web.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Models;

namespace CloudStorage.Web;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(j =>
        {
            j.JsonSerializerOptions.Converters.Add(
                new JsonStringEnumConverter());
        });
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(
                "v1",
                new Microsoft.OpenApi.Models.OpenApiInfo
                {Title = "CloudStorageAPI", Version = "v1"});

            var xmlFilename = 
                $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

            options.IncludeXmlComments(
                Path.Combine(AppContext.BaseDirectory, xmlFilename));

            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = SecuritySchemeType.OAuth2.GetDisplayName()
                        }
                    },
                    new List<string>()
                }
            });
        });

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Audience = "api";
                options.Authority = "https://demo.duendesoftware.com";
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateLifetime = true,
                    ValidateAudience = false
                };
            });

        services
            .AddData(Configuration)
            .AddCore();

        services.AddHostedService<MigrationHostedService>();
    }

    public void Configure(
        IApplicationBuilder app, IWebHostEnvironment environment)
    {
        app.UseCors(builder =>
        {
            builder.AllowAnyOrigin();
        });

        if (environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(
                    "/swagger/v1/swagger.json", "CloudStorageAPI v1");
            });
        }

        app.UseMiddleware<ExceptionMiddleware>();

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseEndpoints(endpoints => 
            endpoints.MapControllers());
    }
}

