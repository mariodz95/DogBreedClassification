using DogBreed.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Autofac;
using DogBreed.Service;
using DogBreed.Service.Common;
using Microsoft.AspNetCore.Identity;
using DogBreed.Repository.Common;
using DogBreed.Repository;
using AutoMapper;
using DogBreedML.Model;
using Microsoft.Extensions.ML;

namespace DogBreed
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // This will all go in the ROOT CONTAINER and is NOT TENANT SPECIFIC.
            builder.RegisterType<ResultService>().As<IResultService>();
            builder.RegisterType<AccountService>().As<IAccountService>();
            builder.RegisterType<AccountRepository>().As<IAccountRepository>();
            builder.RegisterType<ResultRepository>().As<IResultRepository>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new RepositoryProfile());
            });

            var mapper = config.CreateMapper();
            builder.RegisterInstance(mapper).As<IMapper>();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void  ConfigureServices(IServiceCollection services)
        {

            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<DogBreedContext>()
                    .AddDefaultTokenProviders();

            services.AddPredictionEnginePool<ModelInput, ModelOutput>()
                .FromFile(modelName: "DogBreedModel", filePath: "MLModels/MLModel.zip", watchForChanges: true);

            services.AddControllers();
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


            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());


            ////Make sure you call this before calling app.UseMvc()
            //app.UseCors("ApiCorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
