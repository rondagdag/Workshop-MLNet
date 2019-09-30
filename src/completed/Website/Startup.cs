using System;
using System.Collections.Generic;
using System.IO;
using GithubIssueClassifier.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.ML;

namespace Website
{
    public class Startup
    {

        private IHostEnvironment Environment { get; }

        public Startup(IHostEnvironment environment)
        {
            Environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddRouting();

            var modelFilePath = Path.Combine(Environment.ContentRootPath, "GithubClassifier.zip");

            services
                .AddPredictionEnginePool<GithubIssue, GithubIssuePrediction>()
                .FromFile(modelName: "GithubIssueClassifier", filePath: modelFilePath);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
