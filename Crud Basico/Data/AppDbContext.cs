using Crud_Basico.Models;
using Microsoft.EntityFrameworkCore;

namespace Crud_Basico.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}

    public DbSet<Usuario> Usuarios { get; set; }
}
