using AutoMapper;
using VacationRental.Domain.Models;

namespace VacationRental.Api.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddSingleton<IDictionary<int, Rental>>(new Dictionary<int, Rental>());
            services.AddSingleton<IDictionary<int, Booking>>(new Dictionary<int, Booking>());
            return services;
        }

        public static IServiceCollection AddCustomAutoMapper(this IServiceCollection services) =>
               services
                    .AddAutoMapper(typeof(BookingsController).Assembly)
                            .AddSingleton(provider => new MapperConfiguration(cfg =>
                            {       
                                cfg.CreateMap<BookingBindingRequestModel, CreateBookingCommand>();
                                cfg.CreateMap<GetCalendarRequestModel, GetCalendarQuery>();
                                cfg.CreateMap<RentalBindingRequestModel, CreateRentalCommand>();

                                cfg.CreateMap<ResourceId, ResourceIdViewModel>();
                                cfg.CreateMap<Booking, BookingViewModel>();
                                cfg.CreateMap<Calendar, CalendarViewModel>();
                                cfg.CreateMap<Rental, RentalViewModel>();

                            }).CreateMapper());
    }
}
