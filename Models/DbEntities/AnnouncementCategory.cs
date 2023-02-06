using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DbEntities
{
    public class AnnouncementCategory : BaseEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public bool IsScrapperWorking { get; set; }
        public IEnumerable<Announcement> Announcements { get; set; }
        public int WebServiceId { get; set; }
        public virtual WebService WebService { get; set; }
    }
}
