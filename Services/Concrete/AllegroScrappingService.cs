using Data.Repos;
using Microsoft.Extensions.DependencyInjection;
using Models.DbEntities;
using Newtonsoft.Json.Linq;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Services.Concrete
{
    public class AllegroScrappingService : IAllegroScrappingService
    {
        private readonly HttpClient _client;
        private readonly IServiceProvider _services;

        public AllegroScrappingService(IServiceProvider services)
        {
            _client = new HttpClient();
            _services = services;
        }

        public async Task StartScrapping(AnnouncementCategory category)
        {
            using (var scope = _services.CreateScope())
            {
                var _announcementCategoryService = scope.ServiceProvider.GetRequiredService<IAnnouncementCategoryService>();
                var _allegroTokenService = scope.ServiceProvider.GetRequiredService<IAllegroTokenService>();
                var _announcementRepository = scope.ServiceProvider.GetRequiredService<IGenericRepository<Announcement>>();
                await _announcementCategoryService.StartScrapping(category.Id);
                var token = await _allegroTokenService.GetLatest(1);
                var builder = new UriBuilder("https://api.allegro.pl/sale/products");
                builder.Port = -1;
                var query = HttpUtility.ParseQueryString(builder.Query);
                var phrase = category.Url.Substring(category.Url.IndexOf("string=") + 7);
                query["phrase"] = phrase.Replace("%20", " ");
                builder.Query = query.ToString();
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.AccessToken);
                _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/vnd.allegro.public.v1+json"));
                var response = await _client.GetAsync(builder.ToString());
                JObject responseJson = JObject.Parse(await response.Content.ReadAsStringAsync());
                var productsList = JArray.Parse(responseJson["products"].ToString());
                foreach (var product in productsList)
                {
                    var announcement = new Announcement();
                    announcement.AllegroId = product["id"].ToString();
                    announcement.AnnouncementCategoryId = category.Id;
                    announcement.Name = product["name"].ToString();
                    announcement.Description = "";
                    if (product["description"].Type != JTokenType.Null)
                    {
                        var desc = JArray.Parse(product["description"]["sections"].ToString());
                        foreach (var item in desc)
                        {
                            if (item["items"][0]["type"].ToString() == "TEXT")
                            {
                                announcement.Description = announcement.Description + " " + item["items"][0]["content"].ToString();
                            }
                        }
                    }
                    var images = JArray.Parse(product["images"].ToString());
                    if(images.Count > 0)
                    {
                        announcement.Image = product["images"][0]["url"].ToString();
                    }
                    if (await _announcementRepository.Find(x => x.AllegroId == announcement.AllegroId) == null)
                    {
                        await _announcementRepository.Insert(announcement);
                    }
                }
                while (responseJson["nextPage"].Type != JTokenType.Null)
                {
                    query = HttpUtility.ParseQueryString(builder.Query);
                    query["page.id"] = responseJson["nextPage"]["id"].ToString();
                    builder.Query = query.ToString();
                    response = await _client.GetAsync(builder.ToString());
                    responseJson = JObject.Parse(await response.Content.ReadAsStringAsync());
                    productsList = JArray.Parse(responseJson["products"].ToString());
                    foreach (var product in productsList)
                    {
                        var announcement = new Announcement();
                        announcement.AllegroId = product["id"].ToString();
                        announcement.AnnouncementCategoryId = category.Id;
                        announcement.Name = product["name"].ToString();
                        announcement.Description = "";
                        if (product["description"].Type != JTokenType.Null)
                        {
                            var desc = JArray.Parse(product["description"]["sections"].ToString());
                            foreach (var item in desc)
                            {
                                if (item["items"][0]["type"].ToString() == "TEXT")
                                {
                                    announcement.Description = announcement.Description + " " + item["items"][0]["content"].ToString();
                                }
                            }
                        }
                        var images = JArray.Parse(product["images"].ToString());
                        if (images.Count > 0)
                        {
                            announcement.Image = product["images"][0]["url"].ToString();
                        }
                        if (await _announcementRepository.Find(x => x.AllegroId == announcement.AllegroId) == null)
                        {
                            await _announcementRepository.Insert(announcement);
                        }
                    }
                }
                await _announcementCategoryService.StopScrapping(category.Id);
            }
        }
    }
}
