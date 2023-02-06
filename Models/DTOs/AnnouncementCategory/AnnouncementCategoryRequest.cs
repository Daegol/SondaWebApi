using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.AnnouncementCategory
{
    public class AnnouncementCategoryRequest
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int WebServiceId { get; set; }
    }
}
