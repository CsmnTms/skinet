using API.Helpers;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config; // underscore means it's a private field

        public Startup(IConfiguration config)
        {
            _config = config;
        }


        // This method gets called by the runtime. Use this method to add services to the container. 
        // Referred to as the Dependency Injection Container. 
        // Any services we want to add to our app that we then want to make available to other parts of our app, we add to this method.
        // order does not matter here
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers();
            // gets the connection from the appsetings.dev.json
            services.AddDbContext<StoreContext>(x => 
                x.UseSqlite(_config.GetConnectionString("DefaultConnection")) 
            ); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // This is where we add middleware (which can do various things to requests)
        // order matters here
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection(); // if a request comes on the http url, it redirects it to the https

            app.UseRouting(); // responsible for getting us to the controller that we're hitting

            app.UseStaticFiles();

            app.UseAuthorization();

            // maps the endpoints in the controller so the API know where to send the requests to us
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
