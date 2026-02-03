using Microsoft.EntityFrameworkCore;
using Task4.Server.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. ADD SERVICES TO THE CONTAINER (THE "PLUMBING")
// ==============================================================================

// This tells the app to use your PostgreSQL connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ==============================================================================
// 2. BUILD THE APP (FINISH THE HOUSE)
// ==============================================================================
var app = builder.Build();

// 3. CONFIGURE THE HTTP REQUEST PIPELINE
// ==============================================================================

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();