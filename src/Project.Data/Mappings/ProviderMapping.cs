using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Business.Models;

namespace Project.Data.Mappings;

public class ProviderMapping: IEntityTypeConfiguration<Provider>
{
    public void Configure(EntityTypeBuilder<Provider> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasColumnType("varchar(200)");

        builder.Property(p => p.Document)
            .IsRequired()
            .HasColumnType("varchar(14)");

        // 1 : 1 => Provider : Address
        builder.HasOne(f => f.Address)
            .WithOne(e => e.Provider);

        // 1 : N => Provider : Products
        builder.HasMany(f => f.Products)
            .WithOne(p => p.Provider)
            .HasForeignKey(p => p.ProviderId);

        builder.ToTable("Providers");
    }
}
