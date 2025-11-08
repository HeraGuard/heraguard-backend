using heraguard.API.Middleware;
using heraguard.Application.Activities.Interfaces;
using heraguard.Application.Auth.Commands;
using heraguard.Application.Auth.Interfaces;
using heraguard.Application.Auth.Services;
using heraguard.Application.Mapping;
using heraguard.Application.Medications.Interfaces;
using heraguard.Application.MedicalAppointments.Interfaces;
using heraguard.Application.Prescriptions.Interfaces;
using heraguard.Application.Relationships.Interfaces;
using heraguard.Application.Users.Interfaces;
using heraguard.Infrastructure.Data;
using heraguard.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Supabase;

var builder = WebApplication.CreateBuilder(args);

// Controladores
builder.Services.AddControllers();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AutoMapper
builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);
builder.Services.AddAutoMapper(typeof(MedicationProfile).Assembly);
builder.Services.AddAutoMapper(typeof(PrescriptionProfile).Assembly);
builder.Services.AddAutoMapper(typeof(RelationshipProfile).Assembly);

// MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(LoginCommand).Assembly));


// DbContext
builder.Services.AddDbContext<HeraGuardDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("HeraGuardConnection")));

// Cliente Supabase
builder.Services.AddScoped(provider =>
    new Client(
        builder.Configuration["Supabase:Url"] ?? throw new InvalidOperationException("Supabase URL not configured"),
        builder.Configuration["Supabase:AnonKey"] ??
        throw new InvalidOperationException("Supabase AnonKey not configured")
    ));

// Repositorios
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMedicationRepository, MedicationRepository>();
builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
builder.Services.AddScoped<IMedicalAppointmentRepository, MedicalAppointmentRepository>();
builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
builder.Services.AddScoped<IRelationshipRepository, RelationshipRepository>();
builder.Services.AddScoped<IUserRepository,  UserRepository>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();