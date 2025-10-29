using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
   public partial class DataContext : DbContext
   {
      public DataContext() { }

      public DataContext(DbContextOptions<DataContext> options)
      : base(options)
      {
      }

      //Gerador Adicionar aqui
      
public virtual DbSet<Aluno> Aluno { get; set; }
public virtual DbSet<AlunoStatus> AlunoStatus { get; set; }


      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
      }

   }
}
