using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json.Serialization;

using StockArt.Data;

namespace StockArtExplorer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
             .AddMvc()
             .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            services.AddDbContext<StockArtDBContext>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvcWithDefaultRoute();
        }
    }
}
