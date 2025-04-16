using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Telecom.Biz;
using Telecom.DataBase;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Adicionando o Banco de Dados (DbContext com Npgsql)
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        );

        // Permitir qualquer origem (para desenvolvimento)
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });


        // Registrando os serviços de aplicação
        builder.Services.AddScoped<OperadoraServicoService>();
        builder.Services.AddScoped<ContratoService>();
        builder.Services.AddScoped<FaturaService>();
        builder.Services.AddScoped<DashboardService, DashboardService>();

        // Configuração do JSON para evitar problemas com ciclos de referência
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
            });


        // Adicionando o Swagger e a configuração da API
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        var app = builder.Build();

        // Configurando o pipeline de requisições HTTP
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("CorsPolicy");
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}