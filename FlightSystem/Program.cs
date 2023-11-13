using FlightSystem.Data;
using FlightSystem.Helpers;
using FlightSystem.Interface;
using FlightSystem.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Config API.

    // Add JWT Bearer Token  Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] { }
        }
    });
});

//-------------------------------------------------------------------------------


builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<FlightSystemDBContext>().AddDefaultTokenProviders()
    .AddRoleManager<RoleManager<IdentityRole>>(); ;



//-------------------------------------------------------------------------------


builder.Services.AddDbContext<FlightSystemDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("FlightSystem"));
});



builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<IDocumentInfoService, DocumentInfoService>();
builder.Services.AddScoped<IGroupService, GroupService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


//-------------------------------------------------------------------------------


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireUserRole", policy =>
        policy.RequireRole("User"));

    options.AddPolicy("RequireCrewRole", policy =>
    policy.RequireRole("Crew"));

    options.AddPolicy("RequirePilotRole", policy =>
        policy.RequireRole("Pilot"));

    options.AddPolicy("RequireAdminRole", policy =>
        policy.RequireRole("Admin"));

    // Thêm các ủy quyền khác ở đây....
});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = new List<CultureInfo> { new CultureInfo("en-US") };
    options.SupportedUICultures = new List<CultureInfo> { new CultureInfo("en-US") };
});


builder.Services.AddRazorPages()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

builder.Services.Configure<FormOptions>(x =>
{
    x.MultipartBodyLengthLimit = 52428800; // (50MB)
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };

});


builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod())
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
