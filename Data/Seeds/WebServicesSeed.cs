using Data.Repos;
using Models.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Seeds
{
    public static class WebServicesSeed
    {
        public static async Task SeedAsync(IGenericRepository<WebService> repository)
        {
            var allItems = await repository.GetAll();
            if (allItems.Count == 0)
            {
                var webServices = new List<WebService>();

                webServices.Add(new WebService() { Name = "OtoMoto", Url = "https://www.otomoto.pl" });
                webServices.Add(new WebService() { Name = "Allegro", Url = "https://www.allegro.pl" });
                webServices.Add(new WebService() { Name = "Olx", Url = "https://www.olx.pl" });

                await repository.BulkInsert(webServices);
            }
        }
    }
}
