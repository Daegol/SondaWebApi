using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class WebServiceMap : MappingEntityTypeConfiguration<WebService>
    {
        public override void Configure(EntityTypeBuilder<WebService> builder)
        {
            builder.ToTable("WebServices");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(255);
            builder.Property(p => p.Url).HasMaxLength(255);
            builder.Property(p => p.CreateUTC).HasColumnType("DateTime").HasDefaultValueSql("GetUtcDate()");

            base.Configure(builder);
        }
    }
}
