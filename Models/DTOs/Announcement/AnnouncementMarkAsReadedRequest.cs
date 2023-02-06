using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.Announcement
{
    public class AnnouncementMarkAsReadedRequest
    {
        public List<int> AnnouncementsToMarkAsReaded { get; set; }
    }
}
