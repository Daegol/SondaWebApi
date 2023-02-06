using Models.DbEntities;
using Models.DTOs.AnnouncementCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAnnouncementCategoryService
    {
        Task<AnnouncementCategory> Add(AnnouncementCategory model);
        Task<AnnouncementCategory> Update(AnnouncementCategoryUpdateRequest model);
        Task<bool> Delete(int id);
        Task<IEnumerable<AnnouncementCategory>> GetAll();
        Task<IEnumerable<AnnouncementCategory>> GetAllFromService(int id);
        Task<IEnumerable<AnnouncementCategory>> GetAllWithFavourites();
        Task<IEnumerable<AnnouncementCategory>> GetAllFromServiceWithFavourites(int id);
        Task<AnnouncementCategory> Get(int id);
        Task<AnnouncementCategory> GetWithFavourites(int id);
        Task<AnnouncementCategory> StartScrapping(int id);
        Task<AnnouncementCategory> StopScrapping(int id);
    }
}
