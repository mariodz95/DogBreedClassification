using DogBreed.DAL;
using DogBreedML.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.ML;
using Microsoft.EntityFrameworkCore;

namespace DogBreed
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
            //services.AddDbContextPool<DogBreedContext>(options => options.UseSqlServer(this.Configuration.GetConnectionString("localDb")));

            services.AddControllers();

            services.AddCors(options => options.AddPolicy("CorsPolicy",
                 builder =>
                 {
                     builder.AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                 }));


            services.AddPredictionEnginePool<ModelInput, ModelOutput>()
                .FromFile(modelName: "ModelInput", filePath: "MLModels/MLModel.zip", watchForChanges: true);

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ApiClient";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //Make sure you call this before calling app.UseMvc()
            app.UseCors("ApiCorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ApiClient";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
