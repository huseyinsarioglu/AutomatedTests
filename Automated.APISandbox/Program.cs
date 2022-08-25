using FluentValidation;
using FluentValidation.AspNetCore;
using System.Net;
using Automated.APISandbox.Context;

public class Program 
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Add services to the container.
        ConfigureServices(builder.Services);

        var app = builder.Build();
        

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }

        app.Use((context, next) => CheckApiKey(context, next, builder.Configuration));

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddSingleton<IStudentContext, StudentDummyContext>();

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<Program>();
    }

    static async Task CheckApiKey(HttpContext context, RequestDelegate next, IConfiguration configuration)
    {
        const string ApiKey = "ApiKey";
        var expectedApiKey = configuration.GetSection(ApiKey).Value;
        var actualApiKey = context.Request.Headers[ApiKey].ToString();
        if (!actualApiKey.Equals(expectedApiKey, StringComparison.InvariantCultureIgnoreCase))
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Response.Headers[ApiKey] = actualApiKey;
            await context.Response.WriteAsync($"Invalid ApiKey - {actualApiKey}\nYou must send a valid ApiKey!");
            return;
        }

        await next(context);
    }
}