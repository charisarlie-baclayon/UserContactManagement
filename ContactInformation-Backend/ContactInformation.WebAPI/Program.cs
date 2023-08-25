using ContactInformation.WebAPI.Context;
using ContactInformation.WebAPI.Repositories;
using ContactInformation.WebAPI.Repositories.AddressRepository;
using ContactInformation.WebAPI.Repositories.AuditTrailRepository;
using ContactInformation.WebAPI.Repositories.ContactRepository;
using ContactInformation.WebAPI.Repositories.UserRepository;
using ContactInformation.WebAPI.Services.AddressService;
using ContactInformation.WebAPI.Services.AuditTrailService;
using ContactInformation.WebAPI.Services.AuthenticationService;
using ContactInformation.WebAPI.Services.ContactService;
using ContactInformation.WebAPI.Services.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("log/contactinformation.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add UseSerilog for Logging.
builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllers( options =>
{
    options.ReturnHttpNotAcceptable = true;
})
.AddNewtonsoftJson()
.AddXmlDataContractSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();

    // Add header documentation in Swagger
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Contact Information API",
        Description = "An API for Contact Information Management. Prepared by Charis Arlie L. Baclayon."
    });
    // Feed generated xml api docs to swagger
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                               .AllowAnyHeader() // Allow any header
                               .AllowAnyMethod(); // Allow any HTTP method
                      });
});

// Call ConfigureServices.
ConfigureServices(builder.Services);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ContactInformationDbContext>();
    db.Initialize();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

void ConfigureServices(IServiceCollection services)
{
    // Register the DbContext.
    services.AddDbContext<ContactInformationDbContext>( options =>
    {
        options.UseSqlite(builder.Configuration.GetConnectionString("SqlServer"));
    });

    // Register JWT Authentication
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(builder.Configuration.GetSection("Jwt:Key").Value!)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

    // Register HttpContextAccessor
    services.AddHttpContextAccessor();

    // Register AutoMapper
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    // Register Repositories
    services.AddScoped<IAuditTrailRepository, AuditTrailRepository>();
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IContactRepository, ContactRepository>();
    services.AddScoped<IAddressRepository, AddressRepository>();

    // Register Services
    services.AddScoped<IAuditTrailService, AuditTrailService>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IAuthenticationService, AuthenticationService>();
    services.AddScoped<IContactService, ContactService>();
    services.AddScoped<IAddressService, AddressService>();

}