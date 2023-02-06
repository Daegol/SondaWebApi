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
    public class AllegroTokenMap : MappingEntityTypeConfiguration<AllegroToken>
    {
        public override void Configure(EntityTypeBuilder<AllegroToken> builder)
        {
            builder.ToTable("AllegroTokens");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.CreateUTC).HasColumnType("DateTime").HasDefaultValueSql("GetUtcDate()");

            builder.HasOne(x => x.User)
                .WithMany(x => x.AllegroTokens)
                .HasForeignKey(x => x.UserId);

            base.Configure(builder);
        }
    }
}
