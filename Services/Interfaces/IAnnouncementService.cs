using Models.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAnnouncementService
    {
        Task<Announcement> Add(Announcement model);
        Task<Announcement> Update(Announcement model);
        Task<bool> Delete(int id);
        Task<IEnumerable<Announcement>> GetAll();
        Task<IEnumerable<Announcement>> GetAllFromCategory(int id);
        Task<IEnumerable<Announcement>> GetFavouritesFromCategory(int id);
        Task<IEnumerable<Announcement>> GetAllFavourites();
        Task<Announcement> Get(int id);
        Task<Announcement> AddToFavourites(int id);
        Task<Announcement> RemoveFromFavourites(int id);
        Task<Announcement> MarkAsReaded(int id);
        Task<bool> MarkManyAsReaded(List<int> announcements);
        Task<Announcement> MarkAsUnreaded(int id);
        Task<bool> MarkManyAsUnreaded(List<int> announcements);
    }
}
