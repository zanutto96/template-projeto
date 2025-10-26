using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    public class #Entity#Configuration: IEntityTypeConfiguration<#Entity#>
    {
        public void Configure(EntityTypeBuilder<#Entity#> builder)
        {
            builder.ToTable("#Entity#");
            #EntityConfiguratioPrimaryKey#

            #EntityConfiguratioProperties#
        }
    }
}
