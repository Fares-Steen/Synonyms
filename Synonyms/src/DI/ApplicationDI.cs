using Application.Services.SynonymServices;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationDI;

public static class ApplicationDI
{
    public static void AddApplicationLibrary(this IServiceCollection services)
    {
        services.AddScoped<ICreateSynonymService, CreateSynonymService>();
        services.AddScoped<IReadSynonymService, ReadSynonymService>();
        services.AddScoped<IDeleteSynonymService, DeleteSynonymService>();
    }
}
