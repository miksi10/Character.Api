using CharacterApi.BusinessLogic;
using CharacterApi.BusinessLogic.Mapper;
using CharacterApi.DbContext;
using CharacterApi.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<CharacterDbContext>(options =>
    options.UseSqlServer(@"Data Source=(localdb)\FirstLocalDB;Initial Catalog=CharacterDb;Integrated Security=True"));

builder.Services.AddScoped<ICharacterBusinessLogic, CharacterBusinessLogic>();
builder.Services.AddScoped<IItemBusinessLogic, ItemBusinessLogic>();

builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
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

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//SeedData.EnsureSeedData(app);

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
