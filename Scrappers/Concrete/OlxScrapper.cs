using Data.Repos;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using Microsoft.Extensions.DependencyInjection;
using Models.DbEntities;
using Scrappers.Interface;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrappers.Concrete
{
    public class OlxScrapper : IOlxScrapper
    {
        private readonly IServiceProvider _services;
        public OlxScrapper(IServiceProvider services)
        {
            _services = services;
        }

        public async Task StartScrapping(AnnouncementCategory category)
        {
            using (var scope = _services.CreateScope())
            {
                var _announcementCategoryService = scope.ServiceProvider.GetRequiredService<IAnnouncementCategoryService>();
                var _announcementRepository = scope.ServiceProvider.GetRequiredService<IGenericRepository<Announcement>>();
                await _announcementCategoryService.StartScrapping(category.Id);
                var web = new HtmlWeb();
                var document = web.Load(category.Url);
                var nextPage = document.QuerySelector(@"a[data-testid=""pagination-forward""]");
                var nextPageHref = category.Url;
                do
                {
                    document = web.Load(nextPageHref);
                    var offers = document.QuerySelectorAll(@"div[class=""css-19ucd76""]");

                    foreach (var offer in offers)
                    {
                        Announcement announcement = new Announcement();
                        announcement.Name = GetOfferName(offer);
                        announcement.Url = GetOfferUrl(offer);
                        announcement.Price = GetOfferPrice(offer);
                        announcement.Image = GetOfferImage(announcement.Url);
                        announcement.Description = GetOfferDescription(announcement.Url);
                        announcement.AnnouncementCategoryId = category.Id;

                        if (await _announcementRepository.Find(x => x.Url == announcement.Url) == null)
                        {
                            await _announcementRepository.Insert(announcement);
                        }
                    }
                    nextPage = document.QuerySelector(@"a[data-testid=""pagination-forward""]");
                    if (nextPage != null)
                    {
                        nextPageHref = "https://www.olx.pl" + nextPage.GetAttributeValue("href", null);
                    }
                } while (nextPage != null);

                await _announcementCategoryService.StopScrapping(category.Id);
            }
        } 

        private string GetOfferName(HtmlNode node)
        {
            var titleCell = node.ChildNodes.QuerySelectorAll(@"h6.css-v3vynn-Text.eu5v0x0");

            if (titleCell.Count <= 0)
            {
                return null;
            }

            return titleCell[0].InnerText.Trim();
        }

        private string GetOfferUrl(HtmlNode node)
        {
            var urlCell = node.QuerySelectorAll("a.css-1bbgabe");
            if(urlCell.Count <= 0)
            {
                return null;
            }

            var insUrl = urlCell[0].GetAttributeValue("href", null);
            if(insUrl == null)
            {
                return null;
            }

            if(insUrl[0] == '/')
            {
                var url = "https://www.olx.pl" + urlCell[0].GetAttributeValue("href", null);
                return url;
            }
            

            return insUrl;
        }

        private string GetOfferPrice(HtmlNode node)
        {
            var priceCell = node.QuerySelectorAll(@"p[data-testid=""ad-price""]");

            if(priceCell.Count <= 0)
            {
                return null;
            }

            return priceCell[0].InnerText.Trim();
        }

        private string GetOfferImage(string url)
        {
            if (url == null)
            {
                return null;
            }
            var web = new HtmlWeb();
            var document = web.Load(url);

            var divCell = document.QuerySelectorAll("div.css-158jbzd");

            if(divCell.Count > 0)
            {
                var imageCell = divCell[0].ChildNodes.QuerySelectorAll("img");

                if (imageCell.Count <= 0)
                {
                    return null;
                }

                return imageCell[0].GetAttributeValue("src", null);
            }

            var otoDivCell = document.QuerySelectorAll("div.photo-item");

            if(otoDivCell.Count > 0)
            {
                var imageCell = otoDivCell[0].ChildNodes.QuerySelectorAll("img");
                if (imageCell.Count <= 0)
                {
                    return null;
                }

                return imageCell[0].GetAttributeValue("data-lazy", null);
            }

            return null;
        }

        private string GetOfferDescription(string url)
        {
            if(url == null)
            {
                return null;
            }

            var web = new HtmlWeb();
            var document = web.Load(url);

            var descriptionNode = document.QuerySelectorAll("div.css-g5mtbi-Text");

            if(descriptionNode.Count > 0 )
            {
                return descriptionNode[0].InnerText.Trim();
            }

            descriptionNode = document.QuerySelectorAll("div.offer-description__description");

            if(descriptionNode.Count <= 0)
            {
                return null;
            }

            return descriptionNode[0].InnerText.Trim();
        }

    }
}
