using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Workspace.Application.Immplemntions;
using Workspace.Application.Interfaces;
using Workspace.Application.Services;
using Workspace.Infrastructure;
using Workspace.Infrastructure.Repositories.Immplemntions;
using Workspace.Infrastructure.Repositories.Implemntion;
using Workspace_Management_System.Data;
using Workspace_Management_System.Middlewares;
using Workspace_Managment_System.identity;

var builder = WebApplication.CreateBuilder(args);
#region Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
#endregion

#region Configure Swagger with JWT support
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Workspace Management System API",
        Version = "v1",
        Description = "API for Workspace Management System"
    });

    // Add JWT Authentication to Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Just paste your raw JWT Token below (no need to type 'Bearer ')",
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
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
#endregion
#region Database Configration
builder.Services.AddDbContext<WorkSpaceSysContext>(options => builder.Configuration.GetConnectionString("conn"));

#endregion

#region Identity Configration
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;

    // User settings
    options.User.RequireUniqueEmail = true;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
}

).AddEntityFrameworkStores<WorkSpaceSysContext>().AddDefaultTokenProviders();
#endregion

#region JWT Configuration
var jwtKey = builder.Configuration["Jwt:Key"];
if (string.IsNullOrEmpty(jwtKey))
{
    throw new InvalidOperationException(
        "JWT Key is not configured. Use User Secrets or Environment Variables.");
}

if (jwtKey.Length < 32)
{
    throw new InvalidOperationException(
        "JWT Key must be at least 32 characters long for security.");
}

var key = Encoding.UTF8.GetBytes(jwtKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = !builder.Environment.IsDevelopment(); // Only require HTTPS in production
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero, // Remove delay of token expiration
        RequireExpirationTime = true
    };

    // Add event handlers for better debugging
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Add("Token-Expired", "true");
            }
            return Task.CompletedTask;
        }
    };
});

// Register custom services
builder.Services.AddScoped<IJwtService, JwtService>();
#endregion
#region DI
builder.Services.AddScoped<IAuthServiecs, AuthServiecs>();
builder.Services.AddScoped(typeof(IGenricRepo<>),typeof(GenricRepo<>));
builder.Services.AddSingleton<IStatusLookupService, StatusLookupService>();
builder.Services.AddScoped<IProductRepo, ProductRepo>();
builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<IRoomRepo,RoomRepo>();
builder.Services.AddScoped<IRoomServices, RoomServices>();
builder.Services.AddScoped<IRoomRateServices, RoomRateServices>();
builder.Services.AddScoped<IRoomRateRepo, RoomRateRepo>();
builder.Services.AddScoped<IBookingRepo, BookingRepo>();
builder.Services.AddScoped<IBookingServices, BookingServices>();
builder.Services.AddScoped<IInvoiceRepo, InvoiceRepo>();
builder.Services.AddScoped<IInvoiceServices, InvoiceServices>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
#endregion

var app = builder.Build();

// --- Seed Roles and Admin User Start ---
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    // 1. Ensure Identity Roles exist
    string[] roles = { "Admin", "Customer" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // 2. Ensure Admin user exists and has the Admin role
    var adminEmail = "trail@gmail.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            FullName = "Administrator"
        };
        await userManager.CreateAsync(adminUser, "Maro@123");
    }

    // 3. Guarantee the target account actually holds the "Admin" Role payload for access
    if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
    {
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }
}
// --- Seed Roles and Admin User End ---

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
