using System;
using System.Text;
using AspNet.Security.ApiKey.Providers.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MP.Data;
using MP.Data.Keeper;
using MP.Keeper.Models;
using MP.Keeper.Services;
using MP.Models.User;
using MP.Services;
using Newtonsoft.Json.Serialization;

namespace MP {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddIdentity<AppUser, IdentityRole>(options => {
                    // Password settings
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;

                    // Lockout settings
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                    options.Lockout.MaxFailedAccessAttempts = 3;

                    // User settings
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            
            services.AddMvc(options => {
                    options.ReturnHttpNotAcceptable = true;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddXmlSerializerFormatters()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });
            
            // Add authentication with JWT tokens.
            services.AddAuthentication(options => {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options => {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters() {
                        // Standard configuration.
                        ValidIssuer = Configuration["Auth:Jwt:Issuer"],
                        ValidAudience = Configuration["Auth:Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Configuration["Auth:Jwt:Key"])),
                        ClockSkew = TimeSpan.Zero,
                        
                        //Security switches.
                        RequireExpirationTime = true,
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = true
                    };
                    options.IncludeErrorDetails = true;
                })
                .AddApiKey();

            //services.AddAuthorizationPolicyEvaluator();
            /*services.AddAuthorization(options =>
            {
                options.AddPolicy("CanViewAll", policy =>
                {
                    policy.RequireClaim("Management Level", 
                        "Superadministrator", "Administrator", "Manager", "Cashier", "Inspector");
                });

                
                options.AddPolicy("CanManage", policyBuilder =>
                {
                    policyBuilder.RequireAuthenticatedUser();
                    policyBuilder.RequireClaim("Management Level", 
                        "Superadministrator", "Administrator", "Manager");
                });
                
                options.AddPolicy("AdministratorOnly", policyBuilder =>
                {
                    policyBuilder.RequireRole("Administrators");
                });
                
                options.AddPolicy("DeleteBranch", policy =>
                {
                    policy.RequireClaim("Delete Branch", "Delete Branch");
                });
            });*/
            
            // DBContext
            services.AddDbContext<AppDbContext>(options => {
                options.UseSqlite(
                    Configuration.GetConnectionString("SQLiteConnection"));
            });
            
            // Adding repos
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IKeeperRepository, KeeperRepository>();

            // API versioning
            services.AddApiVersioning(options => {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper, UrlHelper>(implementationFactory => {
                var actionContext = implementationFactory.GetService<IActionContextAccessor>().ActionContext;
                return new UrlHelper(actionContext);
            });

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp"; });
        }

        // Сonfiguring the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                //app.UseDeveloperExceptionPage();
            }

            AutoMapper.Mapper.Initialize(config => {
                config.CreateMap<UserDto, AppUser>();
                config.CreateMap<UserForCreationDto, AppUser>();
                config.CreateMap<AppUser, UserDto>();

                config.CreateMap<RubReportDto, RubReport>();
                config.CreateMap<RubReportForCreationDto, RubReport>();
                config.CreateMap<RubReport, RubReportDto>();
            });

            // For NGINX
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa => {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment()) {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}