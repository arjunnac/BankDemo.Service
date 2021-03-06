using BankDemo.AccountService.DataRepository;
using BankDemo.AccountService.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;

namespace BankDemo.AccountService
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
            //services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("TodoList"));
            // Singleton app db context to store in memory data.
            services.AddSingleton<AppDbContext>();
            services.AddSingleton<ICustomerRepository, CustomerRepository>();
            services.AddSingleton<ITransactionsRepository, TransactionsRepository>();
            services.AddTransient<ITransactionService, TransactionHandler>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                var filePath = Path.Combine(System.AppContext.BaseDirectory, "BankDemo.AccountService.xml");
                c.IncludeXmlComments(filePath);
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BankDemo.AccountService", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BankDemo.AccountService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
