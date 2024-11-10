using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Xml.Linq;
using TeamGosuWebApp.Services;
using TeamGosuWebApp.Utility;
using Microsoft.AspNetCore.Builder.Extensions;

namespace TeamGosuWebApp {
    public class Startup {
        private IWebHostEnvironment _hostingEnvironment;

        public Startup(IWebHostEnvironment hostingEnvironment, IConfiguration configuration) {
            _hostingEnvironment = hostingEnvironment;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc(options => options.EnableEndpointRouting = false);
            
            NewsManager newsManager = new NewsManager(_hostingEnvironment.WebRootPath + "/news", _hostingEnvironment.WebRootPath);
            services.AddSingleton<NewsManager>(newsManager);
            TeamsManager teamsManager = new TeamsManager(_hostingEnvironment.WebRootPath + "/teams");
            services.AddSingleton<TeamsManager>(teamsManager);
            services.AddHostedService<BeefManager>();

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(WebApplication app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = true,
                DefaultContentType = "text/plain"
            });

            app.UseAuthentication();
            
            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.MapHub<BeefHub>("/BeefHub");
        }
    }
}
