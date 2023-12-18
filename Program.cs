using System.Text;
using Ecommerce;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Connecting to the database
builder.Services.AddDbContext<EcomDbContext>(options=>{
    options.UseSqlServer(builder.Configuration.GetConnectionString("myConnections"));
});

//Register an event
builder.Services.AddScoped<Iproduct, ProductServices>();
builder.Services.AddScoped<Iorder, OrderServices>();
builder.Services.AddScoped<Iuser,UserService>();
builder.Services.AddScoped<Ijwt, JwtServices>();
builder.AddSwaggenGenExtension();

//Add authentication

builder.AddAuth();

//add authorization
builder.AdminPolicy();


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


