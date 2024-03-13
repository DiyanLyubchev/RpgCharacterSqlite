using AutoFixture;
using Microsoft.EntityFrameworkCore;
using RpgCharacterSqlite.Context;
using RpgCharacterSqlite.Models;
using RpgCharacterSqlite.Repository.SqlLiteWithMigration.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IGenericRepository<RpgCharacter>, GenericRepository<RpgCharacter>>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/add-random-entity", (IGenericRepository<RpgCharacter> repository) =>
{
    Fixture fixture = new();
    RpgCharacter entity = fixture.Build<RpgCharacter>()
    .Without(x => x.Id)
    .Create();

    return repository.InsertWithSave(entity);
});

app.MapGet("/get", (IGenericRepository<RpgCharacter> repository) =>
{
    return repository.GetAll();
});
app.Run();
