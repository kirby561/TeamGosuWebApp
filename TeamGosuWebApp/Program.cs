using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TeamGosuWebApp {
    public class Program {
        public static void Main(string[] args) {
            BuildWebApp(args).Run();
        }

        public static WebApplication BuildWebApp(string[] args) {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            Startup startup = new Startup(builder.Environment, builder.Configuration);
            startup.ConfigureServices(builder.Services);
            
            WebApplication app = builder.Build();
            startup.Configure(app, app.Environment);

            return app;
        }
    }
}
