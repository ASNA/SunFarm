using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ASNA.QSys.ExpoModel;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace CustomerAppSite
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        void ConfigureMonaLisa(IServiceCollection services)
        {
            MonaLisaServer monaLisaServer = new MonaLisaServer();
            Configuration.GetSection("MonaLisaServer").Bind(monaLisaServer);

            if (monaLisaServer.InProcess &&
                string.Compare(monaLisaServer.HostName, "*LoopBack", true) == 0)
            {
                monaLisaServer.Port = ASNA.QSys.MonaServer.Server.StartService(monaLisaServer.Port);
            }

            //services.AddMonaLisa();
            services.AddSingleton<IMonaLisaServer>(s=> monaLisaServer);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // AJAX will need to read Request.Body to be read synchronously  
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.ConfigureDisplayPagesOptions(Configuration);
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.IsEssential = true; // make the session cookie Essential -- Problematic for DGPR??
            });

            // Session data needs a store. AddMemoryCache activates a local memory-based store. 
            services.AddMemoryCache();

            services.AddRazorPages(razorOptions =>
            {
                razorOptions.Conventions.AddAreaPageRoute("IronViews","/CUSTDSPF", "");
            }).AddMvcOptions (mvcOptions =>
            {
                mvcOptions.ValueProviderFactories.Insert(0, new EditedValueProviderFactory());
            });

            ConfigureMonaLisa(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseSession();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

        }
    }
}
