using Alura.Adopet.API.Dados.Context;
using Alura.Adopet.API.Dados.Repository;
using Alura.Adopet.API.Dominio.Entity;
using Alura.Adopet.API.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);// Criando uma aplica��o Web.

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

//DI
builder.Services.AddControllers().AddNewtonsoftJson(options =>
   options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddScoped<ClienteRepository>()
                .AddScoped<PetRepository>()
                .AddScoped<IEventoService,EventoService>()               
                .AddDbContext<DataBaseContext>(opt => opt.UseInMemoryDatabase("AdopetDB"));

//Habilitando o swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Adicionando servi�os.
#pragma warning disable ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'
#pragma warning disable CS8602 // Desrefer�ncia de uma refer�ncia possivelmente nula.
builder.Services.BuildServiceProvider().GetService<IEventoService>().GenerateFakeDate();
#pragma warning restore CS8602 // Desrefer�ncia de uma refer�ncia possivelmente nula.
#pragma warning restore ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'

var app = builder.Build();

// Ativando o Swagger
app.UseSwagger();

//Endpoints
app.MapPost("/proprietario/add", ([FromServices] ClienteRepository repo, [FromBody] Cliente proprietario) =>
{
    return repo.Adicionar(proprietario);
});

app.MapGet("/proprietario/list", ([FromServices] ClienteRepository repo) =>
{
    return repo.ObterTodos();
});

app.MapPost("/pet/add", ([FromServices] PetRepository repo, [FromBody] Pet pet) => {
    return repo.Adicionar(pet);
});

// Listar todas os pets.
app.MapGet("/pet/list", async ([FromServices] PetRepository repo) =>
{
    return Results.Ok(await repo.ObterTodos());
});

//// Upload de arquivos.
//app.MapPost("/pet/upload", async(IFormFile file) =>
//{

//    string tempfile = CreateTempFile.CreateTempfilePath();
//    using var stream = File.OpenWrite(tempfile);
//    await file.CopyToAsync(stream);
//    return Results.Ok("Arquivo enviado com sucesso");
//});

// Ativando a interface Swagger
app.UseSwaggerUI(
    c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoAPI V1");
        c.RoutePrefix = string.Empty;
    }
);

// Roda a aplica��o
app.Run();
