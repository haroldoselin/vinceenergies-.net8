using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using OrderService.Application.Interfaces;
using OrderService.Infrastructure.Data;
using OrderService.Infrastructure.Messaging;

var builder = WebApplication.CreateBuilder(args);

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Permitir apenas esta origem
              .AllowAnyMethod()  // Permitir qualquer m�todo (GET, POST, etc.)
              .AllowAnyHeader(); // Permitir qualquer cabe�alho
    });
});

// Configura��es SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

// Configura��es MongoDB
builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var mongoConnection = builder.Configuration["MongoDb:ConnectionString"];
    var mongoClient = new MongoClient(mongoConnection);
    var databaseName = builder.Configuration["MongoDb:DatabaseName"];
    return mongoClient.GetDatabase(databaseName);
});

// Inje��o de depend�ncia
builder.Services.AddScoped<IOrderService, OrderService.Infrastructure.Services.OrderService>();

// Consumidor do RabbitMQ
builder.Services.AddHostedService<OrderConsumer>();

// Controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Aplicar a pol�tica de CORS
app.UseCors("PermitirFrontend");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
