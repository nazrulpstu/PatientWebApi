using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using PatientWebApi.Interfaces;
using PatientWebApi.Models;
using PatientWebApi.Provider;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
static IEdmModel GetEdmModel()
{

    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Patients>("Patient");
    return builder.GetEdmModel();
}

var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetService<IConfiguration>();
ConnectionStrings.DbConnection = configuration.GetValue<string>("ConnectionStrings:ConnectionString");


builder.Services.AddControllers().AddOData(opt => opt.AddRouteComponents("odata", GetEdmModel()).Select().Filter().Count().SetMaxTop(25));
builder.Services.AddScoped<IPatient,Patient>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
