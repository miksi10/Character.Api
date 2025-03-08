using CharacterApi.BusinessLogic;
using CharacterApi.BusinessLogic.Mapper;
using CharacterApi.DbContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<CharacterDbContext>(options =>
    options.UseSqlServer(@"Data Source=(localdb)\FirstLocalDB;Initial Catalog=CharacterDb;Integrated Security=True"));

builder.Services.AddScoped<ICharacterBusinessLogic, CharacterBusinessLogic>();
builder.Services.AddScoped<IItemBusinessLogic, ItemBusinessLogic>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

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
