using Data.Repos;
using Models.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Seeds
{
    public static class UserSeed
    {
        public static async Task SeedAsync(IGenericRepository<User> repository)
        {
            var allItems = await repository.GetAll();
            if (allItems.Count() == 0)
            {
                var announcementCategories = new List<User>();

                announcementCategories.Add(new User() { });

                await repository.BulkInsert(announcementCategories);
            }
        }
    }
}
