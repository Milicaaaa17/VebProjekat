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
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ProjekatVeb2.Configuration;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Add email configuration
var emailConfiguration = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfiguration);
builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("EmailConfiguration"));


// Konfigurišite JwtSettings
var jwtSettings = new JwtSettings();
builder.Configuration.GetSection("JwtSettings").Bind(jwtSettings);
builder.Services.AddSingleton(jwtSettings);


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PR522019_WEB2", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});



builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000", "http://localhost:3001", "http://localhost:3002", "https://localhost:3000", "https://localhost:3001", "https://localhost:3002")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials();
        });
});



//baza povezivanje
builder.Services.AddDbContext<ContextDB>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("KonekcijaSaBazomVebProjekat"));
});

// Dodajte IHttpContextAccessor kao servis sa opcijom Scoped.
builder.Services.AddHttpContextAccessor();

// Dodajte vaše repozitorijume
builder.Services.AddScoped<IKorisnikRepository, KorisnikRepository>();
builder.Services.AddScoped<IArtikalRepository, ArtikalRepository>();
builder.Services.AddScoped<IPorudzbinaRepository, PorudzbinaRepository>();

// Dodajte vaše servise
builder.Services.AddScoped<IKorisnikService, KorisnikService>();
builder.Services.AddScoped<IEnkripcijaService, EnkripcijaService>();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<IRegistracijaService, RegistracijaService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IAdministratorService, AdministratorService>();
builder.Services.AddScoped<IArtikalService, ArtikalService>();
builder.Services.AddScoped<IPorudzbinaService, PorudzbinaService>();
builder.Services.AddScoped<IEmailService, EmailService>();




builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"])),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build();

    options.AddPolicy("SamoVerifikovani", policy => policy.RequireClaim("StatusVerifikacije", "Odobren"));

});



// Kreirajte AutoMapper konfiguraciju
var mapperConfig = new MapperConfiguration(cfg =>
{
    // Definišite mapiranja izme?u modela i DTO klasa ovde
    cfg.CreateMap<Korisnik, RegistracijaDTO>().ReverseMap();
    cfg.CreateMap<Korisnik, Registrovanje>().ReverseMap();
    cfg.CreateMap<Korisnik, KorisnikDTO>().ReverseMap();
    cfg.CreateMap<Artikal, ArtikalDTO>().ReverseMap();
    cfg.CreateMap<Artikal, KreirajArtikalDTO>().ReverseMap();
    cfg.CreateMap<Porudzbina, PorudzbinaDTO>().ReverseMap();
    cfg.CreateMap<Porudzbina, KreirajPorudzbinuDTO>().ReverseMap();

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
app.UseCors("MyCorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();