using BookStore.BL.CommandHandlers.AuthorCommandHandlers;
using BookStore.BL.CommandHandlers.BookCommandHandlers;
using BookStore.BL.Interfaces;
using DK_Project.DL.Interfaces;
using DK_Project.DL.Repositories.InMemoryRepositories;
using DK_Project.Extensions;
using DK_Project.HealthChecks;
using DK_Project.Models.Mediatr.Commands;
using DK_Project.Models.Mediatr.Commands.AuthorCommands;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

var logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console(theme:AnsiConsoleTheme.Code)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddSerilog(logger);

// Add services to the container.

builder.Services
    .RegisterRepositories()
    .AddAutoMapper(typeof(Program));

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks()
    .AddCheck<SqlHealthCheck>("SQL Server")
    .AddCheck<CustomHealthCheck>("Custom Check")
    .AddUrlGroup(new Uri("https://google.bg"), name:"Google Service");

builder.Services.AddMediatR(typeof(AddAuthorCommandHandler).Assembly);







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

app.RegisterHealthChecks();

app.Run();
