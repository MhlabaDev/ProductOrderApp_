using Microsoft.EntityFrameworkCore;
using ProductOrderApi.Data;
using ProductOrderApi.Interfaces;
using ProductOrderApi.Repositories;
using ProductOrderApi.Services;
using System.Text.Json.Serialization; 

var builder = WebApplication.CreateBuilder(args);

/// <summary>
/// Configure controllers with JSON options (handles references, pretty-printing)
/// </summary>
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true;
    });

/// <summary>
/// Add Swagger for API documentation and testing
/// </summary>
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/// <summary>
/// Register DbContext with SQL Server using connection string
/// </summary>
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

/// <summary>
/// Register repositories for product, order, and customer
/// </summary>
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

/// <summary>
/// Register services for handling business logic
/// </summary>
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<CustomerService>();

/// <summary>
/// Enable CORS for React frontend (localhost:3000)
/// </summary>
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder => builder
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
    );
});

var app = builder.Build();

/// <summary>
/// Enable Swagger only during development
/// </summary>
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

/// <summary>
/// Serve static files (e.g., product images)
/// </summary>
app.UseStaticFiles();

/// <summary>
/// Apply CORS before authorization
/// </summary>
app.UseCors("AllowReactApp");

/// <summary>
/// Enable authorization middleware
/// </summary>
app.UseAuthorization();

/// <summary>
/// Enforce HTTPS redirection in non-development environments
/// </summary>
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

/// <summary>
/// Map controller routes
/// </summary>
app.MapControllers();

/// <summary>
/// Run the application
/// </summary>
app.Run();
