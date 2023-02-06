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
    public class AllegroTokenService : IAllegroTokenService
    {
        private readonly IGenericRepository<AllegroToken> _repository;

        public AllegroTokenService(IGenericRepository<AllegroToken> repository)
        {
            _repository = repository;
        }

        public async Task<AllegroToken> Add(AllegroToken model, int userId)
        {
            model.UserId = userId;
            return await _repository.Insert(model);
        }

        public async Task<AllegroToken> GetLatest(int userId)
        {
            var tokens = await _repository.GetAll();
            return tokens.Where(x => x.UserId == userId).OrderByDescending(x => x.CreateUTC).FirstOrDefault();
        }

        public async Task<bool> IsTokenValid(int userId)
        {
            var tokens = await _repository.GetAll();
            var token = tokens.Where(x => x.UserId == userId).OrderByDescending(x => x.CreateUTC).FirstOrDefault();
            if(token == null) return false;

            if(token.CreateUTC.AddHours(12) < DateTime.UtcNow) return false;

            return true;
        }
    }
}
