using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Budget.Api.Entities;
using Budget.Api.Extensions;
using Budget.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Budget.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddMvcOptions(o => o.OutputFormatters.Add(
                    new XmlDataContractSerializerOutputFormatter()));

            //ToDo ryk til application.json
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BudgetDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            services.AddDbContext<BudgetDbContext>(o => o.UseSqlServer(connectionString));

            services.AddCors(option => option.AddPolicy("CorsPolicyBudgetApi", builder => {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();

            }));

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ISubAccountRepository, SubAccountRepository>();
            services.AddScoped<IPostingLineRepository, PostingLineRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, BudgetDbContext budgetDbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("CorsPolicyBudgetApi");
            budgetDbContext.EnsureSeedDataForContext();

            app.UseMvc();



            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("WebApi Up and Spinning...");
            });
        }
    }
}
