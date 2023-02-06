using Data.Repos;
using Models.DbEntities;
using Models.DTOs.WebService;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concrete
{
    public class WebServiceService : IWebServiceService
    {
        private readonly IGenericRepository<WebService> _repository;

        public WebServiceService(IGenericRepository<WebService> repository)
        {
            _repository = repository;
        }

        public async Task<WebService> Add(WebService model)
        {
            return await _repository.Insert(model);
        }

        public async Task<bool> Delete(int id)
        {
            var webService = await _repository.Find(x => x.Id == id);
            if (webService != null)
            {
                if (await _repository.Delete(webService) > 0)
                    return true;
            }
            return false;
        }

        public async Task<WebService> Get(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<WebService>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<WebService> Update(WebServiceUpdateRequest model)
        {
            var webService = await _repository.GetById(model.Id);
            webService.Name = model.Name;
            webService.Url = model.Url;
            return await _repository.Update(webService);
        }
    }
}
