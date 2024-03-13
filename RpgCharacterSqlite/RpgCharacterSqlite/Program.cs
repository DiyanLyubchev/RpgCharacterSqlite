using AutoFixture;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using RpgCharacterSqlite.Context;
using RpgCharacterSqlite.Models;
using RpgCharacterSqlite.Repository.SqlLiteWithMigration.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IGenericRepository<RpgCharacter>, GenericRepository<RpgCharacter>>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapPost("/add-random-entity", (IGenericRepository<RpgCharacter> repository) =>
{
    Fixture fixture = new();
    RpgCharacter entity = fixture.Build<RpgCharacter>()
    .Without(x => x.Id)
    .Create();

    return Results.Ok(repository.InsertWithSave(entity));
});

app.MapGet("/get", (IGenericRepository<RpgCharacter> repository) =>
{
    return Results.Ok(repository.GetAll());
});

app.MapDelete("/delete-all", (IGenericRepository<RpgCharacter> repository) =>
{
    repository.DeleteAll();
    return Results.NoContent();
});

app.MapDelete("/delete/{id:int}", (int id, IGenericRepository<RpgCharacter> repository) =>
{
    repository.Delete(id);
    return Results.NoContent();
});

app.Run();
