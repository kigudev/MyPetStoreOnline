using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyPetStore.Web.Services.Abstractions;
using MyPetStore.Web.Services.Implementations;
using MyPetStoreOnline.Data;
using MyPetStoreOnline.Entities;
using MyPetStoreOnline.Services.Abstractions;
using MyPetStoreOnline.Services.Implementations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MyPetStore.Web
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
            services.AddRazorPages();
            services.AddControllersWithViews();

            services.AddDbContext<ApplicationContext>(c => c.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
               .AddDefaultTokenProviders()
               .AddEntityFrameworkStores<ApplicationContext>();

            services.Configure<IdentityOptions>(options => {
                options.SignIn.RequireConfirmedEmail = false;

                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 1;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            });

            // open api - estandar de lectura de APIs
            // swagger - librería agnostica al lenguaje de programación
            // swashbunkle - librería swagger en .net
            // nswag - librería swagger en .net
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "MyPetStore", 
                    Version = "v1",
                    Description = "El Api de mi tienda de mascotas en línea",
                    TermsOfService = new Uri("https://google.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Twitter",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/mypetstore")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under creative commons",
                        Url = new Uri("https://github.com/mypetsore/licence")
                    }
                });

                // Accede al archivo de xml donde se guarda la documentación de nuestros métodos.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                // Une nombre del archivo con el path absoluto donde está alojado nuestro sistema.
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
            });

            services.AddTransient<IShopService, ShopService>();
            services.AddTransient<IReportService, ReportService>();
            services.AddScoped<IFileService, FileService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                   {
                       c.InjectStylesheet("/swagger-ui/custom.css");
                       c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyPetStore v1");
                   }
                );
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
