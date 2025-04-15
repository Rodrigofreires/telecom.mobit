using Microsoft.EntityFrameworkCore;
using Telecom.Biz;
using Telecom.DataBase;

var builder = WebApplication.CreateBuilder(args);

// Adicionando o Banco de Dados (DbContext com Npgsql)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Registrando os servi�os de aplica��o
builder.Services.AddScoped<OperadoraServicoService>();
builder.Services.AddScoped<ContratoService>();
builder.Services.AddScoped<FaturaService>();

// Configura��o do JSON para evitar problemas com ciclos de refer�ncia
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

// Adicionando o Swagger e a configura��o da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurando o pipeline de requisi��es HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
