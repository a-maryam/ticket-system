using Microsoft.EntityFrameworkCore;
using ticket_system.Data;
using ticket_system.Services;

// initialization: kestrel, logging, config
var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddScoped<ColumnService>();
builder.Services.AddScoped<TicketService>();
builder.Services.AddScoped<BoardService>();

builder
    .Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter()
        );
    });

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "ClientPolicy",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod();
        }
    );
});

var app = builder.Build();

// Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("ClientPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
