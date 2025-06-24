using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Service.Implementation;
using TravelAgency.Service.Interface;

namespace TravelAgency.Service.Extentions
{
    public static class Extentions
    {
        public static IServiceCollection RegisterService(this IServiceCollection service) 
        {
            service.AddTransient<IBookingService, BookingService>();
            service.AddTransient<ICustomerService, CustomerService>();
            service.AddTransient<IItineraryService, ItineraryService>();
            service.AddTransient<IItineraryActivityService, ItineraryActivityService>();
            service.AddTransient<IItineraryTravelPackageService, ItineraryTravelPackageService>();
            service.AddTransient<ITravelPackageService,TravelPackageService>();
            service.AddTransient<ITravelActivityService, TravelActivityService>();



            return service;
        }
    }
}
