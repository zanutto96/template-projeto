using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    public class AlunoStatusConfiguration: IEntityTypeConfiguration<AlunoStatus>
    {
        public void Configure(EntityTypeBuilder<AlunoStatus> builder)
        {
            builder.ToTable("AlunoStatus");
            builder.HasKey(p => p.AlunoStatusId);

            


        }
    }
}
