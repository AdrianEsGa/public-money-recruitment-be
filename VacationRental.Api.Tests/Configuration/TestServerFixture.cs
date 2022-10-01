using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

public class TestServerFixture : WebApplicationFactory<Startup>
{

    protected override IHost CreateHost(IHostBuilder builder)
    {

        //builder.ConfigureServices(services =>

        //    services.AddScoped()

        //);

        return base.CreateHost(builder);
    }
}
