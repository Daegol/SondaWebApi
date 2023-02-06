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
    public class AnnouncementMap : MappingEntityTypeConfiguration<Announcement>
    {
        public override void Configure(EntityTypeBuilder<Announcement> builder)
        {
            builder.ToTable("Announcements");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.CreateUTC).HasColumnType("DateTime").HasDefaultValueSql("GetUtcDate()");

            builder.HasOne(x => x.AnnouncementCategory)
                .WithMany(x => x.Announcements)
                .HasForeignKey(x => x.AnnouncementCategoryId);

            base.Configure(builder);
        }
    }
}
