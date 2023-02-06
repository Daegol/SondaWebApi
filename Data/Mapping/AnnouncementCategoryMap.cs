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
    public class AnnouncementCategoryMap : MappingEntityTypeConfiguration<AnnouncementCategory>
    {
        public override void Configure(EntityTypeBuilder<AnnouncementCategory> builder)
        {
            builder.ToTable("AnnouncementCategories");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.CreateUTC).HasColumnType("DateTime").HasDefaultValueSql("GetUtcDate()");

            builder.HasOne(x => x.WebService)
                .WithMany(x => x.AnnouncementCategories)
                .HasForeignKey(x => x.WebServiceId);

            base.Configure(builder);
        }
    }
}
