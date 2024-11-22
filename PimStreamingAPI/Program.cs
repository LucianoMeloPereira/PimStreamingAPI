using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using PimStreamingAPI.Dado.Context;
using PimStreamingAPI.Dado.Repositorios;
using PimStreamingAPI.Servico.Interfaces;
using PimStreamingAPI.Servico.Servicos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Injeção de dependência
builder.Services.AddScoped(typeof(IRepositorioGenerico<>), typeof(RepositorioGenerico<>));
builder.Services.AddScoped<IUsuarioServico, UsuarioServico>();
builder.Services.AddScoped<IPlaylistServico, PlaylistServico>();
builder.Services.AddScoped<IConteudoServico, ConteudoServico>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var videosPath = Path.Combine(Directory.GetCurrentDirectory(), "Videos");
if (!Directory.Exists(videosPath))
{
    Directory.CreateDirectory(videosPath);
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(videosPath),
    RequestPath = "/videos"
});

app.UseCors("PermitirTudo");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
