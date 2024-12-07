using DeskManager.Models;
using DeskManager.Repository;
using DeskManager.Services;
using DeskManager.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseInMemoryDatabase("AppDb"));
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
                IsAvailable = false,
                Reservations = new List<Reservation>()
                {
                    new Reservation
                    {
                        Id = 1,
                        ReservationDate = new DateTime(2024, 11, 1),
                        ReserverName = "John Doe"
                    }
                }
            },
            new Desk
            {
                Id = 2,
                DeskNumber = 2,
                RoomName = "Meeting Room",
                IsAvailable = false,
                Reservations = new List<Reservation>()
                {
                    new Reservation
                    {
                        Id = 2,
                        ReservationDate = new DateTime(2024, 11, 2),
                        ReserverName = "Jane Smith"
                    }
                }
            },
            new Desk
            {
                Id = 3,
                DeskNumber = 3,
                RoomName = "Office Room",
                IsAvailable = false,
                Reservations = new List<Reservation>()
                {
                    new Reservation
                    {
                        Id = 3,
                        ReservationDate = new DateTime(2024, 11, 3),
                        ReserverName = "Michael Johnson"
                    }
                }
            },
            new Desk
            {
                Id = 4,
                DeskNumber = 4,
                RoomName = "Board Room",
                IsAvailable = false,
                Reservations = new List<Reservation>()
                {
                    new Reservation
                    {
                        Id = 4,
                        ReservationDate = new DateTime(2024, 11, 4),
                        ReserverName = "Emily Davis"
                    }
                }
            },
            new Desk
            {
                Id = 5,
                DeskNumber = 5,
                RoomName = "Training Room",
                IsAvailable = false,
                Reservations = new List<Reservation>()
                {
                    new Reservation
                    {
                        Id = 5,
                        ReservationDate = new DateTime(2024, 11, 5),
                        ReserverName = "David Wilson"
                    }
                }
            },
            new Desk
            {
                Id = 6,
                DeskNumber = 6,
                RoomName = "Break Room",
                IsAvailable = false,
                Reservations = new List<Reservation>()
                {
                    new Reservation
                    {
                        Id = 6,
                        ReservationDate = new DateTime(2024, 11, 6),
                        ReserverName = "Olivia Martinez"
                    }
                }
            },
            new Desk
            {
                Id = 7,
                DeskNumber = 7,
                RoomName = "Lounge Area",
                IsAvailable = false,
                Reservations = new List<Reservation>()
                {
                    new Reservation
                    {
                        Id = 7,
                        ReservationDate = new DateTime(2024, 11, 7),
                        ReserverName = "James Anderson"
                    }
                }
            },
            new Desk
            {
                Id = 8,
                DeskNumber = 8,
                RoomName = "Library",
                IsAvailable = false,
                Reservations = new List<Reservation>()
                {
                    new Reservation
                    {
                        Id = 8,
                        ReservationDate = new DateTime(2024, 11, 8),
                        ReserverName = "Sophia Taylor"
                    }
                }
            },
            new Desk
            {
                Id = 9,
                DeskNumber = 9,
                RoomName = "Studio",
                IsAvailable = false,
                Reservations = new List<Reservation>()
                {
                    new Reservation
                    {
                        Id = 9,
                        ReservationDate = new DateTime(2024, 11, 9),
                        ReserverName = "Benjamin Thomas"
                    }
                }
            },
            new Desk
            {
                Id = 10,
                DeskNumber = 10,
                RoomName = "Cafeteria",
                IsAvailable = false,
                Reservations = new List<Reservation>()
                {
                    new Reservation
                    {
                        Id = 10,
                        ReservationDate = new DateTime(2024, 11, 15),
                        ReserverName = "Ava Jackson"
                    }
                }
            },
            new Desk
            {
                Id = 11,
                DeskNumber = 11,
                RoomName = "New Room",
                IsAvailable = true,
                Reservations = new List<Reservation>()
            },
            new Desk
            {
                Id = 12,
                DeskNumber = 12,
                RoomName = "Another Room",
                IsAvailable = true,
                Reservations = new List<Reservation>()
            },
            new Desk
            {
                Id = 13,
                DeskNumber = 13,
                RoomName = "Extra Room",
                IsAvailable = true,
                Reservations = new List<Reservation>()
            },
            new Desk
            {
                Id = 14,
                DeskNumber = 14,
                RoomName = "Additional Room",
                IsAvailable = true,
                Reservations = new List<Reservation>()
            },
            new Desk
            {
                Id = 15,
                DeskNumber = 15,
                RoomName = "Special Room",
                IsAvailable = true,
                Reservations = new List<Reservation>()
            },
            new Desk
            {
                Id = 16,
                DeskNumber = 16,
                RoomName = "Unique Room",
                IsAvailable = true,
                Reservations = new List<Reservation>()
            },
            new Desk
            {
                Id = 17,
                DeskNumber = 17,
                RoomName = "Exclusive Room",
                IsAvailable = true,
                Reservations = new List<Reservation>()
            },
            new Desk
            {
                Id = 18,
                DeskNumber = 18,
                RoomName = "Premium Room",
                IsAvailable = true,
                Reservations = new List<Reservation>()
            },
            new Desk
            {
                Id = 19,
                DeskNumber = 19,
                RoomName = "Deluxe Room",
                IsAvailable = true,
                Reservations = new List<Reservation>()
            },
            new Desk
            {
                Id = 20,
                DeskNumber = 20,
                RoomName = "Luxury Room",
                IsAvailable = true,
                Reservations = new List<Reservation>()
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
