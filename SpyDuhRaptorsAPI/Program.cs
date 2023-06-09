using Microsoft.AspNetCore.Authentication.Negotiate;
using SpyDuhRaptorsAPI.Repositories;

namespace SpyDuhRaptorsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            builder.Services.AddTransient<IRelationshipsRepository, RelationshipsRepository>();
            builder.Services.AddTransient<IAssignmentRepository, AssignmentRepository>();
            builder.Services.AddTransient<IAgencyRepository, AgencyRepository>();
            builder.Services.AddTransient<ICountryRepository, CountryRepository>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IServicesRepository, ServicesRepository>();
            builder.Services.AddTransient<IServicesLookUpRepository, ServicesLookUpRepository>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
                .AddNegotiate();

            builder.Services.AddAuthorization(options =>
            {
                // By default, all incoming requests will be authorized according to the default policy.
                options.FallbackPolicy = options.DefaultPolicy;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}