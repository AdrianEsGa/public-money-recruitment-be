using AutoMapper;

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
                                cfg.CreateMap<CalendarDate, CalendarDateViewModel>();
                                cfg.CreateMap<CalendarBooking, CalendarBookingViewModel>();
                                cfg.CreateMap<PreparationTime, PreparationTimeViewModel>();
                                cfg.CreateMap<Rental, RentalViewModel>();

                            }).CreateMapper());

        public static IServiceCollection AddServices(this IServiceCollection services) =>
                services.AddSingleton<IService, Service>();

        public static IServiceCollection AddRepositories(this IServiceCollection services) =>
                services.AddSingleton<IRepository, Repository>();
    }


}
