

using Microsoft.OpenApi.Models;

namespace FinalBoatSystemRental.API;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("Owner", new OpenApiInfo { Title = "Owner Api", Version = "v1" });
                options.SwaggerDoc("Customer", new OpenApiInfo { Title = "Customer Api", Version = "v1" });
            });




            // Setting UP Configuration
            var configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json")
                                .Build();
            #region Logger Creation  
            // Create Logger
            Log.Logger = new LoggerConfiguration()
           .ReadFrom.Configuration(configuration)
           .WriteTo.Console()
           .CreateLogger();
            #endregion 



            #region DbConfiguration
            //Start  DB Configuration 
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            //End  DB Configuration 
            #endregion

            #region RegisterRepositories

            builder.Services.AddScoped<IAdditionRepository, AdditionRepository>();
            builder.Services.AddScoped<IBoatRepository, BoatRepository>();
            builder.Services.AddScoped<IBoatBookingRepository, BoatBookingRepository>();
            builder.Services.AddScoped<IBoatBookingAdditionRepository, BoatBookingAdditionRepository>();
            builder.Services.AddScoped<ICancellationRepository, CancellationRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
            builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
            builder.Services.AddScoped<IReservationAdditionRepository, ReservationAdditionRepository>();
            builder.Services.AddScoped<ITripRepository, TripRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
            #endregion


            #region JWTConfiguration
            builder.Services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });
            #endregion

            #region MediatorConfiguration

            //Mediator
            builder.Services.AddMediatR(c =>
            {
                c.RegisterServicesFromAssemblies(
                    Assembly.GetAssembly(typeof(LoginCommand)),
                    Assembly.GetAssembly(typeof(Owner))

                    );
            });




            #endregion

            #region Fluent Validations Register  
            // Fluent Validations
            builder.Services.AddValidatorsFromAssemblyContaining<UpdateOwnerWalletCommandValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<LoginCommandValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<RegisterOwnerCommandValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<AddBoatCommandValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<AddTripCommandValidator>();
            #endregion

            //AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));


            builder.Services.Configure<JWT>(configuration.GetSection("Jwt"));


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            // Using SeriLog
            builder.Host.UseSerilog();

            Log.Information("Starting up the application");
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"/swagger/{GlobalVariables.Owner}/swagger.json", "Owner Api");
                    c.SwaggerEndpoint($"/swagger/{GlobalVariables.Customer}/swagger.json", "Customer Api");
                });
            }


            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();


            app.Run();



        }
        catch (Exception ex)
        {
            Log.Fatal(ex.Message, "Application Failed");
        }
        finally
        {
            Log.CloseAndFlush();
        }

    }
}
