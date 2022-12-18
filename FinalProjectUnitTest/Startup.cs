using finalProjectApplication.DefaultServices.CustomerAppServices;
using FinalProjectApplication;
using FinalProjectDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectUnitTest
{
    public class Startup
    {
        public Startup()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<PetCareContext>(option =>
                    option.UseInMemoryDatabase("Host=localhost;Username=postgres;Password=root;Database=FinalProject    "));

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ConfigurationProfile());
            });
            var mapper = config.CreateMapper();

            // Add services to the container.
            serviceCollection.AddSingleton(mapper);
            serviceCollection.AddTransient<IDoctorAppService, DoctorAppService>();
            serviceCollection.AddTransient<ICustomerAppService, CustomerAppService>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }
    }
}
