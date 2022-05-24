﻿using System.Reflection;
using System.Text.Json.Serialization;

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
        });
    }

    public void Configure(
        IApplicationBuilder app, IWebHostEnvironment environment)
    {
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

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseEndpoints(endpoints => 
            endpoints.MapControllers());
    }
}
