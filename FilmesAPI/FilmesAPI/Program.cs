using FilmesAPI.FireStore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<FireStoreConfig>();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

var ptBr = new CultureInfo("pt-BR");

var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(ptBr),
    SupportedCultures = new List<CultureInfo> { ptBr },
    SupportedUICultures = new List<CultureInfo> { ptBr }
};



app.UseRequestLocalization(localizationOptions);


app.UseAuthorization();

app.MapControllers();

app.Run();
