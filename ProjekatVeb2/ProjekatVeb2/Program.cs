using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ProjekatVeb2.Data;
using ProjekatVeb2.DTO;
using ProjekatVeb2.Models;
using ProjekatVeb2.Interfaces.IRepoistory;
using ProjekatVeb2.Interfaces.IServices;
using ProjekatVeb2.Repository;
using ProjekatVeb2.Services;
using Cqrs.Hosts;
using ikvm.runtime;
using SolrNet;
using com.sun.xml.@internal.ws.api.policy;
using ProjekatVeb2.Models.Ispis;
using ProjekatVeb2.JWT;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//baza povezivanje
builder.Services.AddDbContext<ContextDB>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("KonekcijaSaBazomVebProjekat"));
});

// Dodajte vaše repozitorijume
builder.Services.AddScoped<IKorisnikRepository, KorisnikRepository>();

// Dodajte vaše servise

builder.Services.AddScoped<IKorisnikService, KorisnikService>();
builder.Services.AddScoped<IEnkripcijaService, EnkripcijaService>();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<IRegistracijaService, RegistracijaService>();
builder.Services.AddScoped<ILoginService, LoginService>();



// Konfigurišite JwtSettings
var jwtSettings = new JwtSettings();
builder.Configuration.GetSection("JwtSettings").Bind(jwtSettings);
builder.Services.AddSingleton(jwtSettings);




// Kreirajte AutoMapper konfiguraciju
var mapperConfig = new MapperConfiguration(cfg =>
{
    // Definišite mapiranja izme?u modela i DTO klasa ovde
    cfg.CreateMap<Korisnik, RegistracijaDTO>().ReverseMap();
    cfg.CreateMap<Korisnik, Registrovanje>().ReverseMap();
    cfg.CreateMap<Korisnik, KorisnikDTO>().ReverseMap();

    //za sliku 
    cfg.CreateMap<IFormFile, byte[]>().ConvertUsing((file, _, context) =>
    {
        return ConvertIFormFileToByteArray(file, context);
    });

    cfg.CreateMap<byte[], IFormFile>().ConvertUsing((byteArray, _, context) =>
    {
        return ConvertByteArrayToIFormFile(byteArray, context);
    });
});


 byte[] ConvertIFormFileToByteArray(IFormFile file, ResolutionContext context)
{
    if (file != null)
    {
        using var memoryStream = new MemoryStream();
        file.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }
    return null;
}

 IFormFile ConvertByteArrayToIFormFile(byte[] byteArray, ResolutionContext context)
{
    if (byteArray != null && byteArray.Length > 0)
    {
        // Pravimo lažni IFormFile objekat
        return new FormFile(new MemoryStream(byteArray), 0, byteArray.Length, "slika", "slika.jpg");
    }
    return null;
}



// Registrujte AutoMapper konfiguraciju u servisnoj kolekciji
builder.Services.AddSingleton(mapperConfig.CreateMapper());

var app = builder.Build();
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