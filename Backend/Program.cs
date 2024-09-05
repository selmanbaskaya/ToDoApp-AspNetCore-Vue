using Backend.Data;
using Backend.Models;
using Backend.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// PostgreSQL bağlantısını ekle
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection")));

// Servisleri DI container'a ekle
builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<UserService>();

// Şifre hashleme hizmetini ekle
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

// Swagger'ı ekle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TodoAppDotnetVue API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    
    // Swagger middleware'ini kullan
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // Swagger UI'nin endpointini belirt
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoAppDotnetVue API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
