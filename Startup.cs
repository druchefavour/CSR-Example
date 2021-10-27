using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Ibis_CSR_Tool
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
            var settings = new Settings();
            Configuration.Bind(settings);

            services.AddSingleton(settings);

            services.AddAuthentication(sharedOptions =>
            {
                sharedOptions.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                sharedOptions.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //  sharedOptions.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
             .AddCookie();
             //.AddOpenIdConnect(options =>
             //{
             //    options.ClientId = Configuration["okta:ClientId"];
             //    options.ClientSecret = Configuration["okta:ClientSecret"];
             //    options.Authority = Configuration["okta:Issuer"];
             //    options.CallbackPath = "/authorization-code/callback";
             //    options.ResponseType = "code";
             //    options.SaveTokens = true;
             //    options.UseTokenLifetime = false;
             //    options.GetClaimsFromUserInfoEndpoint = true;
             //    options.Scope.Add("openid");
             //    options.Scope.Add("profile");
             //    options.TokenValidationParameters = new TokenValidationParameters
             //    {
             //        NameClaimType = "name"
             //    };
             //});

            services.AddHttpClient(
                /*"OktaUser", c => {
                /*c.BaseAddress = new Uri("https://jo.oktapreview.com/api/v1/users");
                c.DefaultRequestHeaders.Accept.Clear();
                c.DefaultRequestHeaders.Add("Accept", "application/json");
                c.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("SSWS", "00bt8AwDAyik16YYNeZH9Be-ELFSWDxmOumWMnxiyw");
                c.DefaultRequestHeaders.Add("link", "<https://jo.oktapreview.com/api/v1/users?after=000u84fdwawXAiHhfj0h7&limit=25>\";\" rel=\"next\"");}*/
                );

            services.AddControllersWithViews();
            services.AddHttpClient();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddMvc(options =>
            {
            });
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                    });
                options.AddPolicy("StrictPolicy",
                    builder =>
                    {
                        builder.WithOrigins("window.location.origin")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod()
                                            .AllowCredentials();
                    });

            });
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
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
