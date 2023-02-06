using Data.Repos;
using Models.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Seeds
{
    public static class AnnouncementCategoriesSeed
    {
        public static async Task SeedAsync(IGenericRepository<AnnouncementCategory> repository)
        {
            var allItems = await repository.GetAll();
            if (allItems.Count() == 0)
            {
                var announcementCategories = new List<AnnouncementCategory>();

                announcementCategories.Add(new AnnouncementCategory() { Name = "Category1", Url = "https://www.otomoto.pl/costam", WebServiceId = 1 });
                announcementCategories.Add(new AnnouncementCategory() { Name = "Category2", Url = "https://www.allegro.pl/costam", WebServiceId = 2 });
                announcementCategories.Add(new AnnouncementCategory() { Name = "Category3", Url = "https://www.olx.pl/costam", WebServiceId = 3 });

                await repository.BulkInsert(announcementCategories);
            }
        }
    }
}
