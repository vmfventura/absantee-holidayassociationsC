using Microsoft.EntityFrameworkCore;

using Application.Services;
using DataModel.Repository;
using DataModel.Mapper;
using Domain.Factory;
using Domain.IRepository;
using Gateway;
using WebApi.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AbsanteeContext>(opt =>
    //opt.UseInMemoryDatabase("AbsanteeList")
    //opt.UseSqlite("Data Source=AbsanteeDatabase.sqlite")
    opt.UseSqlite(Host.CreateApplicationBuilder().Configuration.GetConnectionString("AbsanteeDatabase"))
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddTransient<IHolidayRepository, HolidayRepository>();
builder.Services.AddTransient<IHolidayFactory, HolidayFactory>();
builder.Services.AddTransient<HolidayMapper>();
builder.Services.AddTransient<HolidayService>();

builder.Services.AddTransient<IHolidayPeriodRepository, HolidayPeriodRepository>();
builder.Services.AddTransient<IHolidayPeriodFactory, HolidayPeriodFactory>();
builder.Services.AddTransient<HolidayPeriodMapper>();
builder.Services.AddTransient<HolidayPeriodService>();

builder.Services.AddTransient<IAssociationRepository, AssociationRepository>();
builder.Services.AddTransient<IAssociationFactory, AssociationFactory>();
builder.Services.AddTransient<AssociationMapper>();
builder.Services.AddTransient<AssociationService>();

builder.Services.AddTransient<AssociationCreatedAmqpGateway>();
builder.Services.AddTransient<AssociationUpdatedAmqpGateway>();

// builder.Services.AddTransient<IProjectFactory, ProjectFactory>();
// builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
// builder.Services.AddTransient<ProjectMapper>();
// builder.Services.AddTransient<ProjectService>();

builder.Services.AddScoped<HolidayService>();
builder.Services.AddScoped<HolidaysAssociationsService>();

builder.Services.AddTransient<HolidayGateway>();
builder.Services.AddTransient<HolidayGatewayUpdate>();

builder.Services.AddSingleton<IRabbitMQHolidayConsumerController, RabbitMQHolidayConsumerController>();
builder.Services.AddSingleton<IRabbitMQAssociationCConsumerController, RabbitMQAssociationCConsumerController>();
builder.Services.AddSingleton<IRabbitMQAssociationUConsumerController, RabbitMQAssociationUConsumerController>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); 

app.UseAuthorization();

var rabbitMQConsumerUpdateService = app.Services.GetRequiredService<IRabbitMQHolidayConsumerController>();
var rabbitMQAssociationCConsumerService = app.Services.GetRequiredService<IRabbitMQAssociationCConsumerController>();
var rabbitMQAssociationUConsumerService = app.Services.GetRequiredService<IRabbitMQAssociationUConsumerController>();

rabbitMQConsumerUpdateService.StartConsuming();
rabbitMQAssociationCConsumerService.StartConsuming();
rabbitMQAssociationUConsumerService.StartConsuming();

app.MapControllers();

app.Run();