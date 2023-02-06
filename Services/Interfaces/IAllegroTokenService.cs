using Models.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAllegroTokenService
    {
        Task<AllegroToken> Add(AllegroToken model, int userId);
        Task<AllegroToken> GetLatest(int userId);
        Task<bool> IsTokenValid(int userId);

    }
}
