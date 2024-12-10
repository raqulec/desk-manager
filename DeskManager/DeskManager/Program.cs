using DeskManager.Models;
using DeskManager.Repository;
using DeskManager.Services;
using DeskManager.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");

//Transactions are not supported by the in-memory store.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseInMemoryDatabase("AppDb");
    options.ConfigureWarnings(warnings =>
        warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning));
});

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSingleton<JwtUtils>();

// Add services to the container.

builder.Services.AddAuthorization();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddControllers();
builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddHealthChecks();

builder.Services.AddScoped<IDeskService, DeskService>();
builder.Services.AddScoped<IDeskRepository, DeskRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated();

    var desks = new List<Desk>()
        {
            new Desk
            {
                Id = 1,
                DeskNumber = 1,
                RoomName = "Conference Room",
                IsAvailable = true,
                Reservations = new List<Reservation>()
                {
                    new Reservation
                    {
                        Id = 1,
                        ReservationDate = new DateTime(2024, 11, 1),
                        ReservedBy = "John Doe"
                    }
                }
            },
            new Desk
            {
                Id = 2,
                DeskNumber = 2,
                RoomName = "Meeting Room",
                IsAvailable = true,
                Reservations = new List<Reservation>()
                {
                    new Reservation
                    {
                        Id = 2,
                        ReservationDate = new DateTime(2024, 11, 2),
                        ReservedBy = "Jane Smith"
                    }
                }
            },
            new Desk
            {
                Id = 3,
                DeskNumber = 3,
                RoomName = "Office Room",
                IsAvailable = true,
                Reservations = new List<Reservation>()
                {
                    new Reservation
                    {
                        Id = 3,
                        ReservationDate = new DateTime(2024, 11, 3),
                        ReservedBy = "Michael Johnson"
                    }
                }
            },
            new Desk
            {
                Id = 4,
                DeskNumber = 4,
                RoomName = "Board Room",
                IsAvailable = true,
                Reservations = new List<Reservation>()
                {
                    new Reservation
                    {
                        Id = 4,
                        ReservationDate = new DateTime(2024, 11, 4),
                        ReservedBy = "Emily Davis"
                    }
                }
            },
            new Desk
            {
                Id = 5,
                DeskNumber = 5,
                RoomName = "Training Room",
                IsAvailable = true,
                Reservations = new List<Reservation>()
                {
                    new Reservation
                    {
                        Id = 5,
                        ReservationDate = new DateTime(2024, 11, 5),
                        ReservedBy = "David Wilson"
                    }
                }
            }
        };

    dbContext.Desks.AddRange(desks);
    dbContext.SaveChanges();
}
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "Hello, World!");
app.MapGet("/secret", (ClaimsPrincipal user) => $"Hello {user.Identity?.Name}. My secret")
    .RequireAuthorization();

app.MapGet("/secret2", () => "This is a different secret!")
    .RequireAuthorization(p => p.RequireClaim("scope", "myapi:secrets"));
app.MapHealthChecks("/health");

app.Run();
