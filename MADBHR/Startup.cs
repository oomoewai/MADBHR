using MADBHR.Helper;
using MADBHR_Data.Models;
using MADBHR_Data.Repository;
using MADBHR_Data.Repository.Base;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MADBHR
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<MADBAdminSolutionContext>(opts =>
            {
                opts.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")//use geometry datatype
                );
            });
            //services.AddDbContext <MADBSolutionContext>
            //  (opt1ions => opt1ions.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
              .AddEntityFrameworkStores<MADBAdminSolutionContext>();
          

            services.AddMvc();

            //services.AddScoped<ClaimsPrincipal>(s => s.GetService<IHttpContextAccessor>().HttpContext.User);
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<MADBHR_Services.Base.IEmployeeServices, MADBHR_Services.EmployeeServices>();
            services.AddTransient<MADBHR_Services.Base.IRelationshipServices, MADBHR_Services.RelationshipServices>();
            services.AddTransient<MADBHR_Services.Base.ISonAndDaughterServices, MADBHR_Services.SonAndDaughterServices>();
            services.AddTransient<MADBHR_Services.Base.ITrainingHistoryServices, MADBHR_Services.TrainingHistoryServices>();
            services.AddTransient<MADBHR_Services.Base.IAwardServices, MADBHR_Services.AwardServices>();
            services.AddTransient<MADBHR_Services.Base.IPunishmentServices, MADBHR_Services.PunishmentServices>();
            services.AddTransient<MADBHR_Services.Base.IInternationalKnowledgeServices, MADBHR_Services.InternationalKnowledgeServices>();
            services.AddTransient<MADBHR_Services.Base.IJobHistoryServices, MADBHR_Services.JobHistoryServices>();
            services.AddTransient<MADBHR_Services.Base.ILeaveEntitlementServices, MADBHR_Services.LeaveEntitlementServices>();
            services.AddTransient<MADBHR_Services.Base.IYearlyBonusServices, MADBHR_Services.YearlyBonusServices>();
            services.AddTransient<MADBHR_Services.Base.IEmployeeDisposalServices, MADBHR_Services.EmployeeDisposalServices>();
            services.AddTransient<MADBHR_Services.Base.IPensionServices, MADBHR_Services.PensionServices>();
            services.AddTransient<MADBHR_Services.Base.IDepartmentServices, MADBHR_Services.DepartmentServices>();
            services.AddTransient<MADBHR_Services.Base.IAccountRegisterServices, MADBHR_Services.AccountRegisterServices>();
            services.AddTransient<MADBHR_Services.Base.ITransferServices, MADBHR_Services.TransferServices>();
            services.Configure<MADBHR_Services.Options.ConnectionStrings>(Configuration.GetSection(nameof(MADBHR_Services.Options.ConnectionStrings)));
            services.Configure<Pagination>(Configuration.GetSection("Pagination"));

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("CorsPolicy",
            //        builder => builder.AllowAnyOrigin()
            //        .AllowAnyMethod()
            //        .AllowAnyHeader());

            //});
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = ".AspNetCore.Cookies";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                options.SlidingExpiration = true;
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/AccountLogin/Login";

            });

            services.AddControllersWithViews().AddRazorRuntimeCompilation();

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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //Note the middleware order.
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=AccountLogin}/{action=Login}/{id?}");
            });
        }
    }
}
