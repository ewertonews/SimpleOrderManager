using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OrderManager.Business;
using OrderManager.Business.Factories;
using OrderManager.Business.OrderQueuing;
using OrderManager.Data;
using OrderManager.Data.Factories;

namespace OrderManager
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OrderManager", Version = "v1" });
            });

            services.AddSingleton(typeof(IQueuingHandler<>), typeof(QueuingHandler<>));
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderHandler, OrderHandler>();
            services.AddScoped<IOrderQueue, OrderQueue>();
            services.AddScoped<IOrderQueuingResultFactory, OrderQueuingResultFactory>();
            services.AddScoped<IOrderRecordFactory, OrderRecordFactory>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderManager v1");
                });
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
