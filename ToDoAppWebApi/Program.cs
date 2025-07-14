using Microsoft.EntityFrameworkCore;
using static ToDoAppWebApi.Data.AuthDbContext;

var builder = WebApplication.CreateBuilder(args);

// DbContext'i ekle
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MainConnection")));

// CORS'u etkinleştir (Angular uygulamasından gelen isteklere izin vermek için)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Geliştirme ortamında Swagger'ı kullan
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS middleware'ini ekle
app.UseCors();

app.UseAuthorization(); // Auth yok ama sırası önemli değil bu örnekte

app.MapControllers();

app.Run();