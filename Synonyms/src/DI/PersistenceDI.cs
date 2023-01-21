using Application.IRepositories;
using FakePersistence;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationDI
{
    public static class PersistenceDI
    {
        public static void AddPersistence(this IServiceCollection services)
        {
            services.AddSingleton<ISynonymsRepository, SynonymsRepository>();
        }
    }
}
