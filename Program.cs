using Microsoft.EntityFrameworkCore;
using GerenciadorPedidosAPI.Data;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços de controle e JSON serializer settings
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); // Evita ciclos de referência

builder.Services.AddEndpointsApiExplorer();

// Configuração do Swagger com JWT e informações de contato
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Gerenciador de Pedidos API",
        Version = "v1",
        Description = "API para gerenciar pedidos, clientes e produtos.",
        Contact = new OpenApiContact
        {
            Name = "Igor",
            Email = "damascenoigu@gmail.com",
            Url = new Uri("https://github.com/IgorDamasceno10"),
        }
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Insira o token JWT no formato Bearer {token}",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

// Configurar a conexão com o banco de dados MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 25))));

// Configuração do CORS para liberar requisições de determinados domínios
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
        policy.WithOrigins("https://dominioautorizado.com", "https://outrodominio.com") // Ajuste as origens permitidas
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

// Configura o pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin"); // Aplica a política de CORS

app.UseAuthentication();  // Habilita autenticação JWT
app.UseAuthorization();

app.MapControllers();

app.Run();
