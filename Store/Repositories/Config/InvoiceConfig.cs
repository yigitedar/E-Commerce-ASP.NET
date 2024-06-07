using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Config
{
    public class InvoiceConfig : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(i => i.InvoiceId);
            builder.Property(i => i.InvoiceDate).IsRequired();
            builder.Property(i => i.TotalAmount).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(i => i.ProductDetails).IsRequired();

            builder.HasOne(i => i.Order)
                .WithMany()
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
