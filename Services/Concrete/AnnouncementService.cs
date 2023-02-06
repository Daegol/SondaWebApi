using Data.Repos;
using Models.DbEntities;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concrete
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IGenericRepository<Announcement> _repository;

        public AnnouncementService(IGenericRepository<Announcement> repository)
        {
            _repository = repository;
        }
        public async Task<Announcement> Add(Announcement model)
        {
            return await _repository.Insert(model);
        }

        public async Task<bool> Delete(int id)
        {
            var announcement = await _repository.Find(x => x.Id == id);
            if(announcement != null)
            {
                if(await _repository.Delete(announcement) > 0)
                    return true;
            }
            return false;
        }

        public async Task<Announcement> Get(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<Announcement>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<IEnumerable<Announcement>> GetAllFromCategory(int id)
        {
            var announcements = await _repository.GetAll();
            return announcements.Where(x => x.AnnouncementCategoryId == id);
        }

        public async Task<IEnumerable<Announcement>> GetFavouritesFromCategory(int id)
        {
            var announcements = await _repository.GetAll();
            return announcements.Where(x => x.AnnouncementCategoryId == id && x.IsFavourite == true);
        }

        public async Task<IEnumerable<Announcement>> GetAllFavourites()
        {
            var announcements = await _repository.GetAll();
            return announcements.Where(x => x.IsFavourite == true).OrderBy(x => x.AnnouncementCategoryId);
        }

        public async Task<Announcement> Update(Announcement model)
        {
            return await _repository.Update(model);
        }

        public async Task<Announcement> AddToFavourites(int id)
        {
            var announcement = await _repository.GetById(id);
            announcement.IsFavourite = true;
            return await _repository.Update(announcement);
        }

        public async Task<Announcement> RemoveFromFavourites(int id)
        {
            var announcement = await _repository.GetById(id);
            announcement.IsFavourite = false;
            return await _repository.Update(announcement);
        }

        public async Task<Announcement> MarkAsReaded(int id)
        {
            var announcement = await _repository.GetById(id);
            announcement.IsReaded = true;
            return await _repository.Update(announcement);
        }

        public async Task<bool> MarkManyAsReaded(List<int> announcements)
        {
            foreach(int id in announcements)
            {
                var announcement = await _repository.GetById(id);
                announcement.IsReaded = true;
                var result = await _repository.Update(announcement);
                if(result == null)
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<Announcement> MarkAsUnreaded(int id)
        {
            var announcement = await _repository.GetById(id);
            announcement.IsReaded = false;
            return await _repository.Update(announcement);
        }

        public async Task<bool> MarkManyAsUnreaded(List<int> announcements)
        {
            foreach (int id in announcements)
            {
                var announcement = await _repository.GetById(id);
                announcement.IsReaded = false;
                var result = await _repository.Update(announcement);
                if (result == null)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
