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

// [FIX] Allow the React frontend to talk to this API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy
        .WithOrigins("http://localhost:5173", "http://localhost:5174")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

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
// [FIX] Turn on the CORS policy we created above
app.UseCors("AllowReactApp");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();