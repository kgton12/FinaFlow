using FinaFlow.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinaFlow.Api.Data.Mappings
{
    public class TransactionMapping : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                .IsRequired(true)
                .HasColumnType("varchar(80)")
                .HasMaxLength(80);

            builder.Property(t => t.Type)
                .IsRequired(true)
                .HasColumnType("int");

            builder.Property(t => t.Amount)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)");

            builder.Property(t => t.CreatedAt)
                .IsRequired(true);

            builder.Property(t => t.PaidOrReceivedAt)
                .IsRequired(false);

            builder.Property(t => t.UserId)
                .IsRequired(true)
                .HasColumnType("varchar(160)")
                .HasMaxLength(160);
        }
    }
}
