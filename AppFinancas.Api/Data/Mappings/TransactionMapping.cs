using AppFinancas.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppFinancas.Api.Data.Mappings;

public class TransactionMapping : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transaction");

        builder.HasKey(trans => trans.Id);

        builder.Property(trans => trans.Title)
               .IsRequired()
               .HasColumnType("NVARCHAR")
               .HasMaxLength(80);

        builder.Property(trans => trans.Type)
               .IsRequired()
               .HasColumnType("SMALLINT");

        builder.Property(trans => trans.Amount)
               .IsRequired()
               .HasColumnType("MONEY");
        
        builder.Property(trans => trans.CreatedAt)
               .IsRequired();

        builder.Property(trans => trans.PaidOrReceivedAt)
               .IsRequired();

        builder.Property(trans => trans.UserId)
               .IsRequired()
               .HasColumnType("VARCHAR")
               .HasMaxLength(160);
    } 
}
