using CharacterApi.Authorization;
using CharacterApi.BusinessLogic;
using CharacterApi.BusinessLogic.Mapper;
using CharacterApi.DbContext;
using CharacterApi.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CharacterDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CharacterDb")));

// Added because of user id in JWT token validation
builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AccountDb")));

//Added because we need usermanager instance in ValidUserIdRequirementHandler
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>();

//DI for HttpContextAccessor object we are using in GameMasterOrCharacterOwnerRequirementHandler and Character BL
builder.Services.AddHttpContextAccessor();

//register business logic classes
builder.Services.AddScoped<ICharacterBusinessLogic, CharacterBusinessLogic>();
builder.Services.AddScoped<IItemBusinessLogic, ItemBusinessLogic>();

//register repository classes
builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();

//Custom policies for validation Character owner and valid user id in JWT token
builder.Services.AddScoped<IAuthorizationHandler, GameMasterOrCharacterOwnerRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, ValidUserIdRequirementHandler>();

//register auto mapper profile class
builder.Services.AddAutoMapper(typeof(MappingProfile));

//Bearer authentification
builder.Services.AddAuthentication(options => 
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["TokenAuth:ValidIssuer"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenAuth:IssuerSigningKey"]))
    };
});

//Bearer authorization with custom created policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("GameMasterOrCharacterOwner", policy => policy.Requirements.Add(new GameMasterOrCharacterOwnerRequirement("GameMaster")));
    options.AddPolicy("ValidUser", policy => policy.Requirements.Add(new ValidUserIdRequirement()));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Logging with serilog
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

//Creatinh DB scheme and insert some demo data
//SeedData.EnsureSeedData(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//default serilog middleware
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
