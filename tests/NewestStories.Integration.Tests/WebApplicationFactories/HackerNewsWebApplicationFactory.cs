using NewestStories.Services.Interfaces;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

using Moq;

namespace NewestStories.Integration.Tests.WebApplicationFactories
{
    public class HackerNewsWebApplicationFactory: WebApplicationFactory<Program>
    {
        public Mock<IHackerNewsClient> HackerNewsClientMock = new Mock<IHackerNewsClient>();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(IHackerNewsClient));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddSingleton(HackerNewsClientMock.Object);
            });
        }
    }
}
