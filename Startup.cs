using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OCP_Factory_Strategy.BLL;
using OCP_Factory_Strategy.BLL.Enum;
using OCP_Factory_Strategy.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCP_Factory_Strategy
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

            services.AddTransient<FawryPaymentService>();
            services.AddTransient<JumiaPaymentService>();
            services.AddTransient<AmazonPaymentService>();
            services.AddTransient<Func<int, IPaymentService>>(serviceProvider => paymentProviderType => 
            {
                switch (paymentProviderType)
                {
                    case (int)PaymentServiceProviders.Fawry:
                        return serviceProvider.GetService<FawryPaymentService>();
                    case (int)PaymentServiceProviders.Jumia:
                        return serviceProvider.GetService<JumiaPaymentService>();
                    case (int)PaymentServiceProviders.Amazon:
                        return serviceProvider.GetService<AmazonPaymentService>();
                    default:
                        throw new InvalidOperationException();
                }
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
