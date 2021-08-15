using EventBus.Messages.Common;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Report.API.EventBusConsumer;

namespace Report.API
{
    public class Startup
    {
        private IConfigurationRoot _conf { get; }
        private readonly IWebHostEnvironment _env;
        public static IConfiguration Configuration;

        public Startup(IWebHostEnvironment env)
        {
            _env = env;

            _conf = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            Configuration = _conf;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration.GetValue<string>("CacheSettings:ConnectionString");
            });

            string rabbitMQConnectionString = Configuration.GetValue<string>("EventBusSettings:HostAddress");

            // MassTransit configuration
            services.AddMassTransit(config =>
            {
                config.AddConsumer<LocationReportConsumer>();

                config.UsingRabbitMq((context, configuraiton) =>
                {
                    configuraiton.Host(rabbitMQConnectionString);
                    configuraiton.ReceiveEndpoint(EventBusContants.LocationReportQueue, c =>
                    {
                        c.ConfigureConsumer<LocationReportConsumer>(context);
                    });
                });
            });
            services.AddMassTransitHostedService();

            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<LocationReportConsumer>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Report.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Report.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
