using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Funq;
using ServiceStack;
using Mitto.App2Sms.ServiceInterface;
using ServiceStack.OrmLite;
using Mitto.App2Sms.BussinesLogic.DataAccess;
using ServiceStack.Data;
using Mitto.App2Sms.BussinesLogic.Services;
using Mitto.App2Sms.BussinesLogic;
using ServiceStack.Validation;
using Mitto.App2Sms.BussinesLogic.Validators;
using System;

namespace Mitto.App2Sms
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseServiceStack(new AppHost
            {
                AppSettings = new NetCoreAppSettings(Configuration)
            });
        }
    }

    public class AppHost : AppHostBase
    {
        public AppHost() : base("App2Sms Web API", 
            typeof(SmsApi).Assembly) { }

        public override void Configure(Container container)
        {
            SetConfig(new HostConfig
            {
                DefaultRedirectPath = "/metadata",
                DebugMode = AppSettings.Get(nameof(HostConfig.DebugMode), false)
            });

            string dbConnString = DBManager.GetDBConnString(Configuration);
            IDbConnectionFactory dbFactory = new OrmLiteConnectionFactory(dbConnString, MySqlDialect.Provider);
            container.Register<IDbConnectionFactory>(c => dbFactory);

            Plugins.Add(new ValidationFeature());
            Plugins.Add(new CorsFeature(
                allowOriginWhitelist: new[] { Configuration["FEURL"] },
                allowCredentials: true)
                );

            container.RegisterValidators(typeof(SendSmsValidator).Assembly);

            container.AddScoped<CountryService>();
            container.AddScoped<SmsService>();
            container.AddScoped<StatisticService>();

            DBManager.CreateDb();
            DBManager.InitializeDb(dbFactory);
        }
    }
}
