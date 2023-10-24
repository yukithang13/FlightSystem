using FlightSystem.Data;
using FlightSystem.Interface;
using FlightSystem.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//-------------------------------------------------------------------------------


builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<FlightSystemDBContext>().AddDefaultTokenProviders()
    .AddRoleManager<RoleManager<IdentityRole>>(); ;


//-------------------------------------------------------------------------------


builder.Services.AddDbContext<FlightSystemDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("FlightSystem"));
});



//-------------------------------------------------------------------------------

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IAccountService, AccountService>();



//-------------------------------------------------------------------------------


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireUserRole", policy =>
        policy.RequireRole("User"));


    options.AddPolicy("RequireAdminRole", policy =>
        policy.RequireRole("AdminRole"));

    // Thêm các ủy quyền khác ở đây....
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
