using Application;
using FluentValidation;
using Infrastructure.Http;
using Infrastructure.Logger.Configurtion;
using WebApi.Background;
using WebApi.Configuration;
using WebApi.ErrorHandling;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMemoryCacheConfiguration();

//Set configuration
var productSourceSection = builder.Configuration.GetSection(AppConstants.Configuration.ProductsSourceSection);
builder.Services.Configure<ProductsSourceConfig>(productSourceSection);

builder.Logging.AddLogger(builder.Configuration);

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

//Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

//Add FluentValidator
builder.Services.AddValidatorsFromAssembly(typeof(AppConstants).Assembly);

var app = builder.Build();
//Add Global exception handler
app.UseExceptionHandler(exceptionHandlerApp => exceptionHandlerApp.ConfigureExceptionHandler());

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
