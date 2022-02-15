using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Middleware.Middlewares;

namespace Middleware
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Middleware", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Middleware v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

          //  app.Run(async context => System.Console.WriteLine("Middleware 1"));
            //app.Run(async context => System.Console.WriteLine("Middleware 2"));
            //middleware 2 çalışmaz kısa devre olur 
            /*
           **** asenkron fonks
            bağımsız çalışmak demekdşr ve beklemez. 
            next.invoke diyildiği zaman sonrali methodlar çalışır ve sonucu dönmesi beklenmeden alt satıra geçer
            await dediğimiz zaman sonucun dönülmesi beklenir.

            database ve mail sunucuları gibi dış sistem oldugu için asenkron kullandığımız zamna dikkatli olmamız gerekmektedir.

            alt satırda yapmak işlenen işlem
            */
           
           /* app.Use(async(context,next)=>
            {
                System.Console.WriteLine("Middleware 1 başladı");
                await next.Invoke();
                System.Console.WriteLine("Middleware 1 bitti");
            });

            app.Use(async(context,next)=>
            {
                System.Console.WriteLine("Middleware 2 başladı");
                await next.Invoke();
                System.Console.WriteLine("Middleware 2 bitti");
            });
            app.Use(async(context,next)=>
            {
                System.Console.WriteLine("Middleware 3 başladı");
                await next.Invoke();
                System.Console.WriteLine("Middleware 3 bitti");
            });*/

            app.UseHello();
            
             app.Use(async(context,next)=>
            {
                System.Console.WriteLine("Use Middleware tetiklendi");
                await next.Invoke();
            });
            //Map : yönlendirme(routa) adresinde göre istek gelirsemiddlewareleri yönetmemizi sağlıyor
            app.Map("/example",internalApp=>
            internalApp.Run(async context =>{
                System.Console.WriteLine("/example middleware tetiklendi");
                await context.Response.WriteAsync("/example middleware tetiklendi");
            }));
            app.MapWhen(x=>x.Request.Method=="GET",internallApp=> internallApp.Run(async context => {
                Console.WriteLine("Mapwhen middleware tetiklendi");
                await context.Response.WriteAsync("Mapwhen middleware tetiklendi");
            }));
           /*
           ekran çıktısı sırasıyla 1, 2, 3 başladı daha sonra 3, 2, 1 bitti şeklinde olacaktır
           */ 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
