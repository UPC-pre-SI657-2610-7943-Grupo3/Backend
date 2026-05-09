using HomeLink.InCleanHome.API.Booking.Application.Internal.CommandServices;
using HomeLink.InCleanHome.API.Booking.Application.Internal.QueryServices;
using HomeLink.InCleanHome.API.Booking.Domain.Repositories;
using HomeLink.InCleanHome.API.Booking.Domain.Services;
using HomeLink.InCleanHome.API.Booking.Infrastructure.Persistence.EFC.Repositories;
using HomeLink.InCleanHome.API.IAM.Application.Internal.CommandServices;
using HomeLink.InCleanHome.API.IAM.Application.Internal.OutboundServices;
using HomeLink.InCleanHome.API.IAM.Application.Internal.QueryServices;
using HomeLink.InCleanHome.API.IAM.Domain.Repositories;
using HomeLink.InCleanHome.API.IAM.Domain.Services;
using HomeLink.InCleanHome.API.IAM.Infrastructure.Hashing.BCrypt.Services;
using HomeLink.InCleanHome.API.IAM.Infrastructure.Persistence.EFC.Repositories;
using HomeLink.InCleanHome.API.IAM.Infrastructure.Pipeline.Middleware.Extensions;
using HomeLink.InCleanHome.API.IAM.Infrastructure.Tokens.JWT.Configuration;
using HomeLink.InCleanHome.API.IAM.Infrastructure.Tokens.JWT.Services;
using HomeLink.InCleanHome.API.IAM.Interfaces.ACL;
using HomeLink.InCleanHome.API.IAM.Interfaces.ACL.Services;
using HomeLink.InCleanHome.API.Payments.Application.Internal.CommandServices;
using HomeLink.InCleanHome.API.Payments.Application.Internal.QueryServices;
using HomeLink.InCleanHome.API.Payments.Domain.Repositories;
using HomeLink.InCleanHome.API.Payments.Domain.Services;
using HomeLink.InCleanHome.API.Payments.Infrastructure.Persistence.EFC.Repositories;
using HomeLink.InCleanHome.API.Profiles.Application.ACL;
using HomeLink.InCleanHome.API.Profiles.Application.Internal.CommandServices;
using HomeLink.InCleanHome.API.Profiles.Application.Internal.QueryServices;
using HomeLink.InCleanHome.API.Profiles.Domain.Repositories;
using HomeLink.InCleanHome.API.Profiles.Domain.Services;
using HomeLink.InCleanHome.API.Profiles.Infrastructure.Persistence.EFC.Repositories;
using HomeLink.InCleanHome.API.Profiles.Interfaces.ACL;
using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Application.Internal.CommandServices;
using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Application.Internal.QueryServices;
using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Repositories;
using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Services;
using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Infrastructure.Persistence.EFC.Repositories;
using HomeLink.InCleanHome.API.SearchAndCatalog.Application.Internal.CommandServices;
using HomeLink.InCleanHome.API.SearchAndCatalog.Application.Internal.QueryServices;
using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Repositories;
using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Services;
using HomeLink.InCleanHome.API.SearchAndCatalog.Infrastructure.Persistence.EFC.Repositories;
using HomeLink.InCleanHome.API.Shared.Domain.Repositories;
using HomeLink.InCleanHome.API.Shared.Infrastructure.Interfaces.ASP.Configuration;
using HomeLink.InCleanHome.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using HomeLink.InCleanHome.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ============================================================================
// Routing & Controllers
// ============================================================================
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

// ============================================================================
// CORS — open policy (refine later by environment)
// ============================================================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// ============================================================================
// Database (PostgreSQL via Npgsql.EntityFrameworkCore.PostgreSQL)
// ============================================================================
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

if (connectionString.StartsWith("postgres://"))
{
    var uri = new Uri(connectionString);
    var userInfo = uri.UserInfo.Split(':');
    connectionString = $"Host={uri.Host};Port={uri.Port};Database={uri.LocalPath.TrimStart('/')};Username={userInfo[0]};Password={userInfo[1]};SslMode=Prefer;TrustServerCertificate=true;";
}

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
        options.UseNpgsql(connectionString)
               .LogTo(Console.WriteLine, LogLevel.Information)
               .EnableSensitiveDataLogging()
               .EnableDetailedErrors();
    else
        options.UseNpgsql(connectionString)
               .LogTo(Console.WriteLine, LogLevel.Error);
});

// ============================================================================
// Swagger / OpenAPI
// ============================================================================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "HomeLink.InCleanHome.API",
        Version = "v1",
        Description = "InCleanHome — Domestic Service Hiring Platform API (HomeLink)",
        TermsOfService = new Uri("https://incleanhome.pe/tos"),
        Contact = new OpenApiContact { Name = "HomeLink", Email = "contact@incleanhome.pe" },
        License  = new OpenApiLicense { Name = "Apache 2.0", Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html") }
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Id = "Bearer", Type = ReferenceType.SecurityScheme }
            },
            Array.Empty<string>()
        }
    });
    options.EnableAnnotations();
});

// ============================================================================
// Dependency Injection — per Bounded Context
// ============================================================================

// Shared
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// IAM (User Management) BC
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();

// Profiles BC
builder.Services.AddScoped<IClientProfileRepository, ClientProfileRepository>();
builder.Services.AddScoped<IWorkerProfileRepository, WorkerProfileRepository>();
builder.Services.AddScoped<IClientProfileCommandService, ClientProfileCommandService>();
builder.Services.AddScoped<IClientProfileQueryService, ClientProfileQueryService>();
builder.Services.AddScoped<IWorkerProfileCommandService, WorkerProfileCommandService>();
builder.Services.AddScoped<IWorkerProfileQueryService, WorkerProfileQueryService>();
builder.Services.AddScoped<IProfilesContextFacade, ProfilesContextFacade>();

// SearchAndCatalog BC
builder.Services.AddScoped<IServiceCategoryRepository, ServiceCategoryRepository>();
builder.Services.AddScoped<IWorkerServiceRepository, WorkerServiceRepository>();
builder.Services.AddScoped<IAvailabilitySlotRepository, AvailabilitySlotRepository>();
builder.Services.AddScoped<IServiceCategoryCommandService, ServiceCategoryCommandService>();
builder.Services.AddScoped<IServiceCategoryQueryService, ServiceCategoryQueryService>();
builder.Services.AddScoped<IWorkerServiceCommandService, WorkerServiceCommandService>();
builder.Services.AddScoped<IWorkerServiceQueryService, WorkerServiceQueryService>();
builder.Services.AddScoped<IAvailabilitySlotCommandService, AvailabilitySlotCommandService>();
builder.Services.AddScoped<IAvailabilitySlotQueryService, AvailabilitySlotQueryService>();

// Booking BC
builder.Services.AddScoped<IBookingRequestRepository, BookingRequestRepository>();
builder.Services.AddScoped<IBookingRequestCommandService, BookingRequestCommandService>();
builder.Services.AddScoped<IBookingRequestQueryService, BookingRequestQueryService>();

// Payments BC
builder.Services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
builder.Services.AddScoped<IMonthlyCommissionRepository, MonthlyCommissionRepository>();
builder.Services.AddScoped<IPaymentMethodCommandService, PaymentMethodCommandService>();
builder.Services.AddScoped<IPaymentMethodQueryService, PaymentMethodQueryService>();
builder.Services.AddScoped<IMonthlyCommissionCommandService, MonthlyCommissionCommandService>();
builder.Services.AddScoped<IMonthlyCommissionQueryService, MonthlyCommissionQueryService>();

// ReviewsAndEvaluation BC
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IProfileReportRepository, ProfileReportRepository>();
builder.Services.AddScoped<IReviewCommandService, ReviewCommandService>();
builder.Services.AddScoped<IReviewQueryService, ReviewQueryService>();
builder.Services.AddScoped<IProfileReportCommandService, ProfileReportCommandService>();
builder.Services.AddScoped<IProfileReportQueryService, ProfileReportQueryService>();

var app = builder.Build();

// ============================================================================
// Database initialization
// ============================================================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// ============================================================================
// HTTP request pipeline
// ============================================================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllPolicy");
app.UseRequestAuthorization();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
