using Microsoft.EntityFrameworkCore;
using ProvaPub.Data;
using ProvaPub.Data.Repository;
using ProvaPub.Services;
using ProvaPub.Services;
using ProvaPub.Services.Payment;
using ProvaPub.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TestDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ctx")));

builder.Services.AddScoped<IRandomNumberRepository, RandomNumberRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Service
builder.Services.AddScoped<RandomService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<OrderService>();

//Strategy-Service
builder.Services.AddTransient<IPaymentMethod, PixPayment>();
builder.Services.AddTransient<IPaymentMethod, CreditCardPayment>();
builder.Services.AddTransient<IPaymentMethod, PayPalPayment>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
