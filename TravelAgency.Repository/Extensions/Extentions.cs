using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Models;
using TravelAgency.Repository.Implementation;
using TravelAgency.Repository.Interface;

namespace TravelAgency.Repository.Extensions
{
    public static class Extentions
    {
        public static IServiceCollection RegisterRepository(this IServiceCollection services) 
        {
            services.AddTransient<IBookingRepository, BookingRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IItineraryActivityRepository, ItineraryActivityRepository>();
            services.AddTransient<IItineraryRepository, ItineraryRepository>();
            services.AddTransient<IItineraryTravelPackageRepostitory, ItineraryTravelPackageRepository>();
            services.AddTransient<ITravelActivityRepository, TravelActivityRepository>();
            services.AddTransient<ITravelPackageRepository, TravelPackageRepository>();

            return services;
        }
    }
}
