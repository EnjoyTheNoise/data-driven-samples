using AutoMapper;
using DataDrivenSamples.Data.Blob;
using DataDrivenSamples.Data.CosmosDB;
using DataDrivenSamples.Data.CosmosDB.Mongo;
using DataDrivenSamples.Data.SQL;
using DataDrivenSamples.Data.SQL.UnitOfWork;
using DataDrivenSamples.Data.TableStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataDrivenSamples
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddAutoMapper();

            services.AddDbContext<SqlDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SQL")));

            services.AddTransient<ITableStorage, TableStorage>();
            services.AddTransient<ISqlService, SqlService>();
            services.AddTransient<IBlobStorage, BlobStorage>();
            services.AddTransient<ICosmosDb, CosmosDb>();
            services.AddTransient<IMongoDb, MongoDb>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
