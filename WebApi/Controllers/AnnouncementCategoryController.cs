using AutoMapper;
using Caching.Interfaces;
using Core.Exceptions;
using Data.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Models.DbEntities;
using Models.DTOs.AnnouncementCategory;
using Models.ResponseModels;
using Scrappers.Interface;
using Services.Concrete;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WebApi.Attributes;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementCategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICacheManager _cache;
        private readonly IServiceProvider _services;

        public AnnouncementCategoryController(IMapper mapper, ICacheManager cache, IServiceProvider provider)
        {
            _mapper = mapper;
            _cache = cache;
            _services = provider;
        }

        [HttpGet("getAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<IEnumerable<AnnouncementCategoryDto>>))]
        public async Task<IActionResult> GetAll()
        {
            using (var scope = _services.CreateScope())
            {
                var _service = scope.ServiceProvider.GetRequiredService<IAnnouncementCategoryService>();
                var _scrappingService = scope.ServiceProvider.GetRequiredService<IAllegroScrappingService>();
                var _tokenService = scope.ServiceProvider.GetRequiredService<IAllegroTokenService>();
                var _olxScrapper = scope.ServiceProvider.GetRequiredService<IOlxScrapper>();
                var data = _mapper
                .Map<IEnumerable<AnnouncementCategory>, IEnumerable<AnnouncementCategoryDto>>(await _service.GetAll());
                StartOlxScrapping();
                if (!await _tokenService.IsTokenValid(1))
                {
                    return Ok(new BaseResponse<IEnumerable<AnnouncementCategoryDto>>(data, new ErrorScrapper(456, "Token allegro wygasł. Zaloguj się ponownie"), $"Lista wszystkich kategorii"));
                }
                StartAllegroScrapping();

                return Ok(new BaseResponse<IEnumerable<AnnouncementCategoryDto>>(data, $"Lista wszystkich kategorii"));
            }
        }

        [HttpGet("getAllFromService/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<IEnumerable<AnnouncementCategoryDto>>))]
        public async Task<IActionResult> GetAllFromService(int id)
        {
            using (var scope = _services.CreateScope())
            {
                var _service = scope.ServiceProvider.GetRequiredService<IAnnouncementCategoryService>();
                var _scrappingService = scope.ServiceProvider.GetRequiredService<IAllegroScrappingService>();
                var _tokenService = scope.ServiceProvider.GetRequiredService<IAllegroTokenService>();
                var _olxScrapper = scope.ServiceProvider.GetRequiredService<IOlxScrapper>();
                var data = _mapper
                .Map<IEnumerable<AnnouncementCategory>, IEnumerable<AnnouncementCategoryDto>>(await _service.GetAllFromService(id));
                if (id == 2)
                {
                    if (!await _tokenService.IsTokenValid(1))
                    {
                        return Ok(new BaseResponse<IEnumerable<AnnouncementCategoryDto>>(data, new ErrorScrapper(456, "Token allegro wygasł. Zaloguj się ponownie"), $"Lista wszystkich kategorii"));
                    }
                    StartAllegroScrapping();
                }
                if (id == 3)
                {
                    StartOlxScrapping();
                }
                return Ok(new BaseResponse<IEnumerable<AnnouncementCategoryDto>>(data, $"Lista kategorii z serwisu o id {id}"));
            }
        }

        [HttpGet("getAllWithFavourites")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<IEnumerable<AnnouncementCategoryDto>>))]
        public async Task<IActionResult> GetAllWithFavourites()
        {
            using (var scope = _services.CreateScope())
            {
                var _service = scope.ServiceProvider.GetRequiredService<IAnnouncementCategoryService>();
                var _scrappingService = scope.ServiceProvider.GetRequiredService<IAllegroScrappingService>();
                var _tokenService = scope.ServiceProvider.GetRequiredService<IAllegroTokenService>();
                var _olxScrapper = scope.ServiceProvider.GetRequiredService<IOlxScrapper>();
                var data = _mapper
                .Map<IEnumerable<AnnouncementCategory>, IEnumerable<AnnouncementCategoryDto>>(await _service.GetAllWithFavourites());

                StartOlxScrapping();
                if (!await _tokenService.IsTokenValid(1))
                {
                    return Ok(new BaseResponse<IEnumerable<AnnouncementCategoryDto>>(data, new ErrorScrapper(456, "Token allegro wygasł. Zaloguj się ponownie"), $"Lista wszystkich kategorii"));
                }
                StartAllegroScrapping();
                return Ok(new BaseResponse<IEnumerable<AnnouncementCategoryDto>>(data, $"Lista kategorii z ulubionymi ogłoszeniami"));
            }
        }

        [HttpGet("getAllFromServiceWithFavourites/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<IEnumerable<AnnouncementCategoryDto>>))]
        public async Task<IActionResult> GetAllFromServiceWithFavourites(int id)
        {
            using (var scope = _services.CreateScope())
            {
                var _service = scope.ServiceProvider.GetRequiredService<IAnnouncementCategoryService>();
                var _scrappingService = scope.ServiceProvider.GetRequiredService<IAllegroScrappingService>();
                var _tokenService = scope.ServiceProvider.GetRequiredService<IAllegroTokenService>();
                var _olxScrapper = scope.ServiceProvider.GetRequiredService<IOlxScrapper>();
                var data = _mapper
                .Map<IEnumerable<AnnouncementCategory>, IEnumerable<AnnouncementCategoryDto>>(await _service.GetAllFromServiceWithFavourites(id));
                if (id == 2)
                {
                    if (!await _tokenService.IsTokenValid(1))
                    {
                        return Ok(new BaseResponse<IEnumerable<AnnouncementCategoryDto>>(data, new ErrorScrapper(456, "Token allegro wygasł. Zaloguj się ponownie"), $"Lista wszystkich kategorii"));
                    }
                    StartAllegroScrapping();
                }
                if (id == 3)
                {
                    StartOlxScrapping();
                }
                return Ok(new BaseResponse<IEnumerable<AnnouncementCategoryDto>>(data, $"Lista kategorii z ulubionymi ogłoszeniami z serwisu o id {id}"));
            }
        }

        [HttpGet("getOne/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<AnnouncementCategoryDto>))]
        public async Task<IActionResult> GetOne(int id)
        {
            using (var scope = _services.CreateScope())
            {
                var _service = scope.ServiceProvider.GetRequiredService<IAnnouncementCategoryService>();
                var _scrappingService = scope.ServiceProvider.GetRequiredService<IAllegroScrappingService>();
                var _tokenService = scope.ServiceProvider.GetRequiredService<IAllegroTokenService>();
                var _olxScrapper = scope.ServiceProvider.GetRequiredService<IOlxScrapper>();
                var data = _mapper
                .Map<AnnouncementCategory, AnnouncementCategoryDto>(await _service.Get(id));
                if (data == null)
                    throw new ApiException("Kategoria nie istnieje") { StatusCode = (int)HttpStatusCode.BadRequest };
                return Ok(new BaseResponse<AnnouncementCategoryDto>(data, $"Kategoria o numerze id {id}"));
            }
        }

        [HttpGet("getOneWithFavourites/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<AnnouncementCategoryDto>))]
        public async Task<IActionResult> GetOneWithFavourites(int id)
        {
            using (var scope = _services.CreateScope())
            {
                var _service = scope.ServiceProvider.GetRequiredService<IAnnouncementCategoryService>();
                var _scrappingService = scope.ServiceProvider.GetRequiredService<IAllegroScrappingService>();
                var _tokenService = scope.ServiceProvider.GetRequiredService<IAllegroTokenService>();
                var _olxScrapper = scope.ServiceProvider.GetRequiredService<IOlxScrapper>();
                var data = _mapper
                .Map<AnnouncementCategory, AnnouncementCategoryDto>(await _service.GetWithFavourites(id));
                if (data == null)
                    throw new ApiException("Kategoria nie istnieje") { StatusCode = (int)HttpStatusCode.BadRequest };
                return Ok(new BaseResponse<AnnouncementCategoryDto>(data, $"Kategoria z ulubionymi ogłoszeniami o numerze id {id}"));
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<string>))]
        public async Task<IActionResult> Add([FromBody] AnnouncementCategoryRequest request)
        {
            using (var scope = _services.CreateScope())
            {
                var _service = scope.ServiceProvider.GetRequiredService<IAnnouncementCategoryService>();
                var _scrappingService = scope.ServiceProvider.GetRequiredService<IAllegroScrappingService>();
                var _tokenService = scope.ServiceProvider.GetRequiredService<IAllegroTokenService>();
                var _olxScrapper = scope.ServiceProvider.GetRequiredService<IOlxScrapper>();
                _cache.Clear();
                var result = await _service.Add(_mapper.Map<AnnouncementCategoryRequest, AnnouncementCategory>(request));
                if (result == null)
                    throw new ApiException("Coś poszło nie tak. Spróbuj ponownie.") { StatusCode = (int)HttpStatusCode.BadRequest };
                return Ok(new BaseResponse<int>(result.Id, "Kategoria została dodana"));
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<string>))]
        public async Task<IActionResult> Update([FromBody] AnnouncementCategoryUpdateRequest request)
        {
            using (var scope = _services.CreateScope())
            {
                var _service = scope.ServiceProvider.GetRequiredService<IAnnouncementCategoryService>();
                var _scrappingService = scope.ServiceProvider.GetRequiredService<IAllegroScrappingService>();
                var _tokenService = scope.ServiceProvider.GetRequiredService<IAllegroTokenService>();
                var _olxScrapper = scope.ServiceProvider.GetRequiredService<IOlxScrapper>();
                _cache.Clear();
                var result = await _service.Update(request);
                if (result == null)
                    throw new ApiException("Coś poszło nie tak. Spróbuj ponownie.") { StatusCode = (int)HttpStatusCode.BadRequest };
                return Ok(new BaseResponse<string>("Kategoria została zaaktualizowana"));
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<string>))]
        public async Task<IActionResult> Delete(int id)
        {
            using (var scope = _services.CreateScope())
            {
                var _service = scope.ServiceProvider.GetRequiredService<IAnnouncementCategoryService>();
                var _scrappingService = scope.ServiceProvider.GetRequiredService<IAllegroScrappingService>();
                var _tokenService = scope.ServiceProvider.GetRequiredService<IAllegroTokenService>();
                var _olxScrapper = scope.ServiceProvider.GetRequiredService<IOlxScrapper>();
                _cache.Clear();
                var result = await _service.Delete(id);
                if (!result)
                    throw new ApiException("Coś poszło nie tak. Spróbuj ponownie.") { StatusCode = (int)HttpStatusCode.BadRequest };
                return Ok(new BaseResponse<string>("Kategoria została usunięta"));
            }
        }

        private async Task StartAllegroScrapping()
        {
            using (var scope = _services.CreateScope())
            {
                var _service = scope.ServiceProvider.GetRequiredService<IAnnouncementCategoryService>();
                var _scrappingService = scope.ServiceProvider.GetRequiredService<IAllegroScrappingService>();
                var _tokenService = scope.ServiceProvider.GetRequiredService<IAllegroTokenService>();
                var _olxScrapper = scope.ServiceProvider.GetRequiredService<IOlxScrapper>();
                var categories = await _service.GetAllFromService(2);
                foreach (var item in categories)
                {
                    if (!item.IsScrapperWorking)
                    {
                        _scrappingService.StartScrapping(item);
                    }
                }
            }
        }

        private async Task StartOlxScrapping()
        {
            using (var scope = _services.CreateScope())
            {
                var _service = scope.ServiceProvider.GetRequiredService<IAnnouncementCategoryService>();
                var _scrappingService = scope.ServiceProvider.GetRequiredService<IAllegroScrappingService>();
                var _tokenService = scope.ServiceProvider.GetRequiredService<IAllegroTokenService>();
                var _olxScrapper = scope.ServiceProvider.GetRequiredService<IOlxScrapper>();
                var categories = await _service.GetAllFromService(3);
                foreach (var item in categories)
                {
                    if (!item.IsScrapperWorking)
                    {
                        _olxScrapper.StartScrapping(item);
                    }
                }
            }
        }
    }
}
