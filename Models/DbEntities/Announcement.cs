using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DbEntities
{
    public class Announcement : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string Image { get; set; }   
        public string Url { get; set; }
        public string AllegroId { get; set; }
        public bool IsFavourite { get; set; }
        public bool IsReaded { get; set; }
        public int AnnouncementCategoryId { get; set; }
        public virtual AnnouncementCategory AnnouncementCategory { get; set; }

    }
}
