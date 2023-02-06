using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DbEntities
{
    public class WebService : BaseEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public IEnumerable<AnnouncementCategory> AnnouncementCategories { get; set; }
    }
}
