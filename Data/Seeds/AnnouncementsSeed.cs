using Data.Repos;
using Models.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Seeds
{
    public static class AnnouncementsSeed
    {
        public static async Task SeedAsync(IGenericRepository<Announcement> repository)
        {
            var allItems = await repository.GetAll();
            if (allItems.Count() == 0)
            {
                var announcements = new List<Announcement>();

                announcements.Add(new Announcement() { Name = "TestAnnouncement1", Url = "http://testUrl.pl/costam", Description = "Opis", Price = "10", IsFavourite = false, AnnouncementCategoryId = 1 });
                announcements.Add(new Announcement() { Name = "TestAnnouncement2", Url = "http://testUrl.pl/costam", Description = "Opis", Price = "11", IsFavourite = false, AnnouncementCategoryId = 1 });
                announcements.Add(new Announcement() { Name = "TestAnnouncement3", Url = "http://testUrl.pl/costam", Description = "Opis", Price = "12", IsFavourite = true, AnnouncementCategoryId = 1 });
                announcements.Add(new Announcement() { Name = "TestAnnouncement4", Url = "http://testUrl.pl/costam", Description = "Opis", Price = "13", IsFavourite = true, AnnouncementCategoryId = 1 });

                announcements.Add(new Announcement() { Name = "TestAnnouncement5", Url = "http://testUrl.pl/costam", Description = "Opis", Price = "10", IsFavourite = false, AnnouncementCategoryId = 2 });
                announcements.Add(new Announcement() { Name = "TestAnnouncement6", Url = "http://testUrl.pl/costam", Description = "Opis", Price = "11", IsFavourite = true, AnnouncementCategoryId = 2 });
                announcements.Add(new Announcement() { Name = "TestAnnouncement7", Url = "http://testUrl.pl/costam", Description = "Opis", Price = "12", IsFavourite = false, AnnouncementCategoryId = 2 });
                announcements.Add(new Announcement() { Name = "TestAnnouncement8", Url = "http://testUrl.pl/costam", Description = "Opis", Price = "13", IsFavourite = false, AnnouncementCategoryId = 2 });

                announcements.Add(new Announcement() { Name = "TestAnnouncement9", Url = "http://testUrl.pl/costam", Description = "Opis", Price = "10", IsFavourite = false, AnnouncementCategoryId = 3 });
                announcements.Add(new Announcement() { Name = "TestAnnouncement10", Url = "http://testUrl.pl/costam", Description = "Opis", Price = "11", IsFavourite = true, AnnouncementCategoryId = 3 });
                announcements.Add(new Announcement() { Name = "TestAnnouncement11", Url = "http://testUrl.pl/costam", Description = "Opis", Price = "12", IsFavourite = true, AnnouncementCategoryId = 3 });
                announcements.Add(new Announcement() { Name = "TestAnnouncement12", Url = "http://testUrl.pl/costam", Description = "Opis", Price = "13", IsFavourite = true, AnnouncementCategoryId = 3 });

                await repository.BulkInsert(announcements);
            }
        }
    }
}
