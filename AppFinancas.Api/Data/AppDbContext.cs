using AppFinancas.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AppFinancas.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{ 
    // o null! (null not) garante que a propriedade não vai ficar sem valores.
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;

    //O Assembly vai varrer o projeto procurando todas as classes
    // que contenham a interface "IEntityTypeConfiguration" implementada.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
       => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    /*
     Outra forma de fazer:

     protected override void OnModelCreating(ModelBuilder modelBuilder) 
     {
        modelBuilder.ApplyConfiguration(new CategoryMapping());
        modelBuilder.ApplyConfiguration(new TransactionMapping());
     }
     */
}
