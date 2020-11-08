using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using playing_with_secrets.data;
using System;
using Vault;

namespace playing_with_secrets
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddVault(options => Configuration.GetSection(nameof(VaultOptions)).Bind(options));
            services.Configure<RapidApiContext>(Configuration.GetSection(nameof(RapidApiContext)));
            services.AddSingleton<IRapidApiSecrets, RapidApiSecrets>();
            services.AddSingleton<IRapidApiOptions, RapidApiOptions>();
            services.AddSingleton<ISecretsRepository, SecretsRepository>();
            services.AddSingleton<IWeatherByZipCodeRepository, WeatherByZipCodeRepository>();
            services.AddControllers();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Playing With Secrets");
                c.RoutePrefix = string.Empty;
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
