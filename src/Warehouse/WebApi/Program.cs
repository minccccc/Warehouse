using Application.Configuration;
using Infrastructure.Http.Configuration;
using Infrastructure.Logger.Configurtion;
using WebApi.Configuration;
using WebApi.ErrorHandling;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddLogger(builder.Configuration);

builder.Services.AddMemoryCacheConfiguration();
builder.Services.AddQuartzConfiguration(builder.Configuration);
builder.Services.AddHttpConfiguration();
builder.Services.AddControllers();
builder.Services.AddMediatrConfiguration();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddFluentValidatorConfiguration();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
