using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaludariumController.Core.Services;
using PaludariumController.Core.Interfaces;
using PaludariumController.InfraStructure.Devices;
using System.Diagnostics;
using System.IO;
using PaludariumController.Core.Utils;

namespace PaludariumController.WebApi
{
    public class Startup
    {
        public string InstanceName { get; private set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            InstanceName = Configuration.GetValue<string>("InstanceName");
            Config.AppSettings.Configuration = Configuration;
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = $"{InstanceName}Controller.WebApi", Version = "v1" });
            });

            services.AddScoped<ILightsService, LightService>();
            services.AddScoped<ITemperatureService, TemperatureService>();

            if (!Directory.Exists(System.IO.Path.Combine(FileUtil.GetWorkingDir(), "files")))
            {
                Directory.CreateDirectory(System.IO.Path.Combine(FileUtil.GetWorkingDir(), "files"));
            }    

            switch (Configuration.GetValue<string>("Device"))
            {
                case "Mock": SetupMock(services); break;
                case "Com": SetupCom(services, Configuration); break;
                default: services.AddScoped<IDevice, MockDevice>(); break;
            }

        }
        private static void SetupCom(IServiceCollection services, IConfiguration configuration)
        {
            var serialPort = new System.IO.Ports.SerialPort();
            serialPort.PortName = configuration.GetValue<string>("Port");
            Console.WriteLine($"{serialPort.PortName} is chosen based on Port variable: {configuration.GetValue<string>("Port")}");
            services.AddSingleton<System.IO.Ports.SerialPort>(serialPort);
            services.AddSingleton<IDevice, ComDevice>();
        }
        private static void SetupMock(IServiceCollection services)
        {
            services.AddSingleton<IDevice, MockDevice>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }

            bool useSwagger = Configuration.GetValue<string>("UseSwagger").ToLower() == "true";

            if (useSwagger)
            {
                Console.WriteLine("Swagger is enabled");
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PaludariumController.WebApi v1"));
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
