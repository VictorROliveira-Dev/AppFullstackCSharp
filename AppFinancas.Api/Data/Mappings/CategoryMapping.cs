using AppFinancas.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppFinancas.Api.Data.Mappings;

public class CategoryMapping : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
       builder.ToTable("Category");

        builder.HasKey(cat => cat.Id);

        builder.Property(cat => cat.Title)
               .IsRequired()
               .HasColumnType("NVARCHAR")
               .HasMaxLength(80);
        
        builder.Property(cat => cat.Description)
               .IsRequired(false)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(255);

        builder.Property(cat => cat.UserId)
               .IsRequired()
               .HasColumnType("VARCHAR")
               .HasMaxLength(160);
    }
}
