using CharacterApi.Authorization;
using CharacterApi.BusinessLogic;
using CharacterApi.BusinessLogic.Mapper;
using CharacterApi.DbContext;
using CharacterApi.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<CharacterDbContext>(options =>
    options.UseSqlServer(@"Data Source=(localdb)\FirstLocalDB;Initial Catalog=CharacterDb;Integrated Security=True"));

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ICharacterBusinessLogic, CharacterBusinessLogic>();
builder.Services.AddScoped<IItemBusinessLogic, ItemBusinessLogic>();

builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();

builder.Services.AddScoped<IAuthorizationHandler, GameMasterOrCharacterOwnerRequirementHandler>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddAuthentication(options => 
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuer = true,
        ValidIssuer = "https://world.of.gamecraft.rs/Account.Api",
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("afsdkjasjflxswafsdklk434orqiwup3457u-34oewir4irroqwiffv48mfs"))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("GameMasterOrCharacterOwner", policy => policy.Requirements.Add(new GameMasterOrCharacterOwnerRequirement("GameMaster")));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

//SeedData.EnsureSeedData(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
