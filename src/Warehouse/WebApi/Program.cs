using Application;
using Application.Features.Products.Synchronize;
using Domain.Common;
using Infrastructure.Http;
using WebApi.Background;
using WebApi.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMemoryCacheConfiguration();

//Set configuration
var productSourceSection = builder.Configuration.GetSection(Constants.Configuration.ProductsSourceSection);
builder.Services.Configure<ProductsSourceConfig>(productSourceSection);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddHostedService<ProductsSyncService>();
builder.Services.AddTransient<IRetrieveProductsService, RetrieveProductsService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add MediatR
builder.Services.AddMediatR(cfg =>
     cfg.RegisterServicesFromAssembly(typeof(AppConstants).Assembly));

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
