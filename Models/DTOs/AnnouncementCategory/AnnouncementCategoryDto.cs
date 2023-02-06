using Models.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.DTOs;

namespace Models.DTOs.AnnouncementCategory
{
    public class AnnouncementCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public IEnumerable<AnnouncementDto> Announcements { get; set; }
        public WebServiceDto WebService { get; set; }
    }
}
