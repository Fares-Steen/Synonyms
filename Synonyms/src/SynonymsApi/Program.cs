using ApplicationDI;

namespace SynonymsApi;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CORSPolicy",
                builder =>
                {
                    builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowAnyOrigin();
                }
            );
        });
        builder.Host.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddConsole();
        });
        builder.Services.AddTransient<ExceptionHandlingMiddleware>();
        builder.Services.AddApplicationLibrary();
        builder.Services.AddPersistence();
        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen();
        var app = builder.Build();
        app.UseCors("CORSPolicy");

        
        if (app.Environment.IsDevelopment())
        {

        }
        app.UseSwagger();
        app.UseSwaggerUI();
        //app.UseHttpsRedirection();

        //app.UseAuthorization();

        app.MapControllers();
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.Run();
    }
}