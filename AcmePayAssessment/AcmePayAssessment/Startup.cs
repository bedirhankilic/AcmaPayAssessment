using AcmePayAssessment.BusinessLayer.Abstract;
using AcmePayAssessment.BusinessLayer.Concretes;
using AcmePayAssessment.DataAccessLayer;
using AcmePayAssessment.DataAccessLayer.Abstract;
using AcmePayAssessment.DataAccessLayer.Concretes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AcmePayAssessment
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
            services.AddDbContext<AcmeDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultLocalConnectionString"));
            });

            //Dependency Injection
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ITransactionService, TransactionsManager>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                AcmeDbContext context = serviceScope.ServiceProvider.GetRequiredService<AcmeDbContext>();
                //DbContext migration process.
                context.Database.Migrate();

            }

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
