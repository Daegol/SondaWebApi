using Models.DbEntities;
using Models.DTOs.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IWebServiceService
    {
        Task<WebService> Add(WebService model);
        Task<WebService> Update(WebServiceUpdateRequest model);
        Task<bool> Delete(int id);
        Task<IEnumerable<WebService>> GetAll();
        Task<WebService> Get(int id);
    }
}
