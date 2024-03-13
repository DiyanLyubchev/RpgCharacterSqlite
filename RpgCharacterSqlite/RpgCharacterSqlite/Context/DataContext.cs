using Microsoft.EntityFrameworkCore;
using RpgCharacterSqlite.Models;

namespace RpgCharacterSqlite.Context
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<RpgCharacter> RpgCharacters { get; set; }
    }
}
