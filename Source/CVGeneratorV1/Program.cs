using AutoMapper;
using BLL.Managers;
using BLL.Profiles;
using BLL.ValidatorInterfaces;
using BLL.Validators;
using DAL.Data;
using DAL.Repositories;
using DAL.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
    options.OutputFormatters.RemoveType<StringOutputFormatter>();
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var provider =  builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();

builder.Services.AddCors(options =>
{
    var frontendURL = configuration.GetValue<string>("fronted_url");

    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins(frontendURL).AllowAnyMethod().AllowAnyHeader();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();                 
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(
    options => options.LowercaseUrls = true
);
builder.Services.AddMvc()
    .AddNewtonsoftJson(
        options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/CVlog.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddDbContext<CVContext>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<PersonManager>();
builder.Services.AddScoped<EducationManager>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ProjectManager>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<CategoryManager>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<SkillManager>();
builder.Services.AddScoped<SkillLevelManager>();
builder.Services.AddScoped<IPersonValidator, PersonValidator>();
builder.Services.AddScoped<IEducationValidator, EducationValidator>();
builder.Services.AddScoped<IProjectValidator, ProjectValidator>();
builder.Services.AddScoped<ISkillValidator, SkillValidator>();
builder.Services.AddScoped<ISkillLevelValidator, SkillLevelValidator>();
builder.Services.AddScoped<ISkillCategoryValidator, SkillCategoryValidator>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
new MapperConfiguration(cfg =>
{
    cfg.AddProfile<PersonProfile>();
    cfg.AddProfile<EducationProfile>();
    cfg.AddProfile<ProjectProfile>();
    cfg.AddProfile<SkillProfile>();
    cfg.AddProfile<SkillLevelProfile>();
    cfg.AddProfile<CategoryProfile>();
}).AssertConfigurationIsValid();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints => 
{ 
    endpoints.MapControllers(); 
});

app.Run();
