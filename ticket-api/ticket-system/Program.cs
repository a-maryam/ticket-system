using Microsoft.EntityFrameworkCore;
using ticket_system.Data;
using ticket_system.Services;

// initialization: kestrel, logging, config
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<TicketService>(); // create ticket service whenever requested

// Dependencies
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

var app = builder.Build();

// Configure the HTTP request pipelineS.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Middleware
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
