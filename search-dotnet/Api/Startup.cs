using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MediatR;
using Application;
using Application.Mappers;
using Application.Filters;
using Application.HttpClients;
using System.Net.Http;
using Brokers;
using Microsoft.EntityFrameworkCore.SqlServer;
using Brokers.Persistence;

namespace Api
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
           
            services.AddControllers();
           
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
            });

            // services.AddSingleton(sp => sp.GetRequiredService<IHttpClientFactory>());
          
            services.AddHttpClient(); 
            services.AddScoped<IHttpClient, HttpClientAdaptor>();
            services.AddScoped<IJustEatSearchRestaurantClient, JustEatSearchRestaurantClient>();
            services.AddScoped<IJsonDeserializer, RestaurantsDeserializer>();
            services.AddScoped<ISearchRestaurantsMapper, SearchRestaurantsMapper>();
            services.AddScoped<IRestaurantsFilter, RestaurantFilter>();
            services.AddScoped<IRestaurantsFilter, RestaurantFilter>();

            services.AddMediatR(typeof(FindRestaurantsByCodeQuery).Assembly);
        
            services.AddBrokers(this.Configuration);
            //TODO: services.AddAutoMapper(new Assembly[] { typeof(RestaurantProfile).GetTypeInfo().Assembly });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurant Search Api v1");
                    c.EnableDeepLinking();
                    c.DisplayOperationId();
                });
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
