using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Hangfire;
using Hangfire.PostgreSql;
using heraguard.API.Middleware;
using heraguard.Application.Activities.Interfaces;
using heraguard.Application.Auth.Commands;
using heraguard.Application.Auth.Interfaces;
using heraguard.Application.Auth.Services;
using heraguard.Application.Mapping;
using heraguard.Application.Medications.Interfaces;
using heraguard.Application.MedicalAppointments.Interfaces;
using heraguard.Application.Notifications.Interfaces;
using heraguard.Application.Prescriptions.Interfaces;
using heraguard.Application.Relationships.Interfaces;
using heraguard.Application.Users.Interfaces;
using heraguard.Infrastructure.BackgroundJobs;
using heraguard.Infrastructure.Data;
using heraguard.Infrastructure.Repositories;
using heraguard.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Supabase;
using heraguard.Application.Chat.Interfaces;
using Firebase.Database;
using heraguard.Application.Sos.Interfaces;

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
builder.Services.AddAutoMapper(typeof(SosProfile).Assembly);

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

// Cliente Firebase
builder.Services.AddScoped(provider =>
{
    var firebaseUrl = builder.Configuration["Firebase:RealtimeDatabaseUrl"]
        ?? throw new InvalidOperationException("Firebase RealtimeDatabaseUrl not configured");
    return new FirebaseClient(firebaseUrl);
});

// Repositorios
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMedicationRepository, MedicationRepository>();
builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
builder.Services.AddScoped<IMedicalAppointmentRepository, MedicalAppointmentRepository>();
builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
builder.Services.AddScoped<IRelationshipRepository, RelationshipRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IChatRepository, FirebaseChatRepository>();

builder.Services.AddScoped<IUserDeviceTokenRepository, UserDeviceTokenRepository>();
builder.Services.AddScoped<IMedicationScheduleRepository, MedicationScheduleRepository>();
builder.Services.AddScoped<IMedicationIntakeLogRepository, MedicationIntakeLogRepository>();
builder.Services.AddScoped<ICaregiverAlertRepository, CaregiverAlertRepository>();
builder.Services.AddScoped<ISosEventRepository, SosEventRepository>();


var firebaseJson = builder.Configuration["Firebase:ConfigJson"];
if (!string.IsNullOrEmpty(firebaseJson))
{
    var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(firebaseJson));
    var options = new AppOptions
    {
        Credential = GoogleCredential.FromStream(stream)
    };
    FirebaseApp.Create(options);
}
else
{
    Console.WriteLine("⚠️ No se encontró el secreto Firebase:ConfigJson");
}

builder.Services.AddHangfire(config =>
    config.UsePostgreSqlStorage(options =>
        options.UseNpgsqlConnection(builder.Configuration.GetConnectionString("HeraGuardConnection"))
    )
);
builder.Services.AddHangfireServer();

builder.Services.AddScoped<IBackgroundJobScheduler, HangfireJobScheduler>();
builder.Services.AddScoped<INotificationService, FirebaseNotificationService>();

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

app.UseHangfireDashboard("/hangfire");

app.UseAuthorization();
app.MapControllers();

app.Run();