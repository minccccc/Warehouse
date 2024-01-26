using Application.Extensions;
using Infrastructure.Extensions;
using WebApi.ErrorHandling;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddLogger(builder.Configuration);

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure();
builder.Services.AddPresentation();

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
