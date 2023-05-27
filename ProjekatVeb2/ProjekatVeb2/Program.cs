using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjekatVeb2.Data;
using ProjekatVeb2.DTO;
using ProjekatVeb2.Interfaces;
using ProjekatVeb2.Mapper;
using ProjekatVeb2.Models;
using ProjekatVeb2.Repository;
using ProjekatVeb2.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IKorisnikRepository, KorisnikRepository>();
builder.Services.AddScoped<IServiceKorisnik, ServiceKorisnik>();
builder.Services.AddScoped<IServiceAutentifikacija, ServiceAutentifikacija>();

builder.Services.AddScoped<IEmailService, ServiceEmail>();
;


builder.Services.AddDbContext<ContextDB>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("KonekcijaSaBazomVebProjekat"));
});

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MapDTO());
    mc.CreateMap<RegistracijaDTO, Korisnik>();
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
