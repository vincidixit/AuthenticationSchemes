using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace UserManagementService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = builder.Configuration["JwtBearerOptions:Authority"];
                options.Audience = builder.Configuration["JwtBearerOptions:Audience"];
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
