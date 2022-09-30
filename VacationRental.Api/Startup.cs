using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.OpenApi.Models;
using VacationRental.Api.Infrastructure.Extensions;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {

        services.AddControllers();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Vacation rental information", Version = "v1" });
        })
            .AddDatabase()
            .AddCustomAutoMapper()
            .AddMediatR(typeof(CreateBookingCommand).Assembly)
            .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<BookingsController>();
                    fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                })
            .AddMvcCore()
            .AddApiExplorer();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        app.UseSwagger();
        app.UseSwaggerUI(opts => opts.SwaggerEndpoint("/swagger/v1/swagger.json", "VacationRental v1"));

    }
}
