using Contacts.API.Services;
using Contacts.API.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<IContactService, ContactService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder =>
        {
            builder
                .WithOrigins(
                    "http://localhost:8080",
                    "http://localhost",
                    "http://contacts.hr",
                    "http://www.contacts.hr",
                    "http://frontend"
                )
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithExposedHeaders("X-Total-Count");
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");
//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run("http://+:80");
