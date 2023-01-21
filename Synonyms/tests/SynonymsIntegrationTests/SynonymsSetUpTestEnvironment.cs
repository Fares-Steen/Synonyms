using Application.IRepositories;
using FakePersistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Program = SynonymsApi.Program;

namespace SynonymsIntegrationTests
{
    public class SynonymsSetUpTestEnvironment:WebApplicationFactory<Program>
    {
        public readonly HttpClient TestClient;
        public readonly ISynonymsRepository SynonymsRepository = new SynonymsRepository();
        public SynonymsSetUpTestEnvironment()
        {
            TestClient = CreateClient();
        }
        
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(serviceCollection =>
            {
                serviceCollection.AddSingleton(SynonymsRepository);
            });
            return base.CreateHost(builder);
        }
    }
}
