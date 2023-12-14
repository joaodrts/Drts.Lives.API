using Application;
using Application.Interfaces;
using Domain.Interfaces.Repositorys;
using Domain.Interfaces.Services;
using Domain.Services;
using Infrastructure.Data;
using Infrastructure.Repositorys;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<AppDbContext>(opt =>
        opt.UseNpgsql(builder.Configuration.GetConnectionString("SampleDbConnection")));

builder.Services.AddScoped<IPersonApplication, ApplicationServicePerson>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

builder.Services.AddScoped<ILiveApplication, ApplicationServiceLive>();
builder.Services.AddScoped<ILiveService, LiveService>();
builder.Services.AddScoped<ILiveRepository, LiveRepository>();

builder.Services.AddScoped<IEnrollmentApplication, ApplicationServiceEnrollment>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsProduction())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
