using Data.Repos;
using Models.DbEntities;
using Models.DTOs.AnnouncementCategory;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concrete
{
    public class AnnouncementCategoryService : IAnnouncementCategoryService
    {
        private readonly IGenericRepository<AnnouncementCategory> _repository;
        public AnnouncementCategoryService(IGenericRepository<AnnouncementCategory> repository)
        {
            _repository = repository;
        }
        public async Task<AnnouncementCategory> Add(AnnouncementCategory model)
        {
            return await _repository.Insert(model);
        }

        public async Task<bool> Delete(int id)
        {
            var announcementCategory = await _repository.Find(x => x.Id == id);
            if (announcementCategory != null)
            {
                if (await _repository.Delete(announcementCategory) > 0)
                    return true;
            }
            return false;
        }

        public async Task<AnnouncementCategory> Get(int id)
        {
            return await _repository.GetByIdWithInclude(id, x => x.Announcements, x => x.WebService);
        }

        public async Task<IEnumerable<AnnouncementCategory>> GetAll()
        {
            return await _repository.GetAllWithInclude(x => x.Announcements, x => x.WebService);
        }

        public async Task<IEnumerable<AnnouncementCategory>> GetAllFromService(int id)
        {
            var announcementCategories = await _repository.GetAllWithInclude(x => x.Announcements, x => x.WebService);
            return announcementCategories.Where(x => x.WebServiceId == id);
        }

        public async Task<IEnumerable<AnnouncementCategory>> GetAllFromServiceWithFavourites(int id)
        {
            var announcementCategories = await GetAll();
            announcementCategories = announcementCategories.Where(x => x.WebServiceId == id);
            foreach (var announcementCategory in announcementCategories)
            {
                announcementCategory.Announcements = announcementCategory.Announcements.Where(x => x.IsFavourite == true);
            }
            return announcementCategories;
        }

        public async Task<IEnumerable<AnnouncementCategory>> GetAllWithFavourites()
        {
            var announcementCategories = await GetAll();
            foreach(var announcementCategory in announcementCategories)
            {
                announcementCategory.Announcements = announcementCategory.Announcements.Where(x => x.IsFavourite == true);
            }
            return announcementCategories;
        }

        public async Task<AnnouncementCategory> GetWithFavourites(int id)
        {
            var announcementCategory = await Get(id);
            announcementCategory.Announcements = announcementCategory.Announcements.Where(x => x.IsFavourite == true);
            return announcementCategory;
        }

        public async Task<AnnouncementCategory> StartScrapping(int id)
        {
            var announcementCategory = await _repository.GetByIdWithInclude(id);
            announcementCategory.IsScrapperWorking = true;
            return await _repository.Update(announcementCategory);
        }

        public async Task<AnnouncementCategory> StopScrapping(int id)
        {
            var announcementCategory = await _repository.GetByIdWithInclude(id);
            announcementCategory.IsScrapperWorking = false;
            return await _repository.Update(announcementCategory);
        }

        public async Task<AnnouncementCategory> Update(AnnouncementCategoryUpdateRequest model)
        {
            var announcementCategory = await _repository.GetById(model.Id);
            announcementCategory.Name = model.Name;
            announcementCategory.Url = model.Url;
            return await _repository.Update(announcementCategory);
        }
    }
}
