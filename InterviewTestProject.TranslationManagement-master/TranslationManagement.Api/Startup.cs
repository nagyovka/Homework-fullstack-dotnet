using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using TranslationManagement.Database;
using TranslationManagement.Database.Repositories.Interfaces;
using TranslationManagement.Database.Repositories.Implementation;
using TranslationManagement.Business.Mappers;
using TranslationManagement.Business.Services.Interfaces;
using TranslationManagement.Business.Services.Implementation;
using TranslationManagement.Api.Validators;
using FluentValidation;
using TranslationManagement.Api.Mappers;

namespace TranslationManagement.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
            services.AddControllers();
            services.AddValidatorsFromAssemblyContaining<TranslationJobUpdateRequestValidator>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TranslationManagement.Api", Version = "v1" });
            });
            services.AddSingleton<TranslatorApiMapper>();
            services.AddSingleton<TranslationJobApiMapper>();

            // Register database layer
            services.AddDbContext<AppDbContext>(options => 
                options.UseSqlite("Data Source=TranslationAppDatabase.db"));
            services.AddScoped<ITranslationJobRepository, TranslationJobRepository>();
            services.AddScoped<ITranslatorRepository, TranslatorRepository>();

            // Register business layer
            services.AddSingleton<TranslationJobMapper>();
            services.AddSingleton<TranslatorMapper>();
            services.AddScoped<ITranslationJobService, TranslationJobService>();
            services.AddScoped<ITranslatorService, TranslatorService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TranslationManagement.Api v1"));

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseCors("AllowAllOrigins");
        }
    }
}
