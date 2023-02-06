using Models.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrappers.Interface
{
    public interface IOlxScrapper
    {
        Task StartScrapping(AnnouncementCategory category);
    }
}
