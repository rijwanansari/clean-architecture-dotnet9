using CleanArchitecture.Application;
using CleanArchitecture.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

// Configure API behavior
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = false;
});

// Add OpenAPI/Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() 
    { 
        Title = "Clean Architecture API", 
        Version = "v1",
        Description = "A Clean Architecture implementation with .NET 9",
        Contact = new() { Name = "API Support", Email = "support@cleanarchitecture.com" }
    });
    
    // Enable XML comments if available
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

// Register Application Layer
builder.Services.AddApplication();

// Register Infrastructure Layer
builder.Services.AddInfrastructure(builder.Configuration);

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Clean Architecture API v1");
        options.RoutePrefix = string.Empty; // Serve Swagger UI at root
    });
}

// Global exception handling
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";
        
        var error = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
        if (error != null)
        {
            var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogError(error.Error, "Unhandled exception occurred");
            
            await context.Response.WriteAsJsonAsync(new
            {
                error = "An error occurred processing your request.",
                details = app.Environment.IsDevelopment() ? error.Error.Message : null
            });
        }
    });
});

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Map Health Checks
app.MapHealthChecks("/health");

app.Run();
