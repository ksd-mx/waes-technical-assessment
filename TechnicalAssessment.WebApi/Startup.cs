﻿// <copyright file="Startup.cs" company="WAES">
//  Copyright (c) All rights reserved.
// </copyright>
namespace TechnicalAssessment.WebApi
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Initial runtime configuration class.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="env">An instance of the <see cref="IHostingEnvironment"/> implementation class.</param>
        public Startup(IHostingEnvironment env)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            this.Configuration = builder.Build();
        }

        /// <summary>
        /// Gets an instance of the <see cref="IConfigurationRoot"/> class
        /// representing the actual configuration settings applied to the runtime
        /// and visible in an application wide scope.
        /// </summary>
        public IConfigurationRoot Configuration { get; private set; }

        /// <summary>
        /// This method gets called by the runtime. 
        /// Use it to add services to the container.
        /// </summary>
        /// <param name="services">An instance of the collection of runtime services
        /// like MVC, Logging and security.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
        }

        /// <summary>
        /// This method gets called by the runtime. 
        /// Use it to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">An instance of the <see cref="IApplicationBuilder"/> implementation class.</param>
        /// <param name="env">An instance of the <see cref="IHostingEnvironment"/> implementation class.</param>
        /// <param name="loggerFactory">An instance of the <see cref="ILoggerFactory"/> implementation class.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            
            app.UseMvc();
        }
    }
}