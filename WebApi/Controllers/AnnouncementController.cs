using AutoMapper;
using Caching.Interfaces;
using Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DbEntities;
using Models.DTOs;
using Models.DTOs.Announcement;
using Models.DTOs.AnnouncementCategory;
using Models.ResponseModels;
using Services.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WebApi.Attributes;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private readonly IAnnouncementService _service;
        private readonly IMapper _mapper;
        private readonly ICacheManager _cache;

        public AnnouncementController(IAnnouncementService announcementService, IMapper mapper, ICacheManager cache)
        {
            _service = announcementService;
            _mapper = mapper;
            _cache = cache;
        }

        [HttpGet("getOne/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<AnnouncementDto>))]
        public async Task<IActionResult> GetOne(int id)
        {
            var data = _mapper
                .Map<Announcement, AnnouncementDto>(await _service.Get(id));
            if (data == null)
                throw new ApiException("Ogłoszenie nie istnieje") { StatusCode = (int)HttpStatusCode.BadRequest };
            return Ok(new BaseResponse<AnnouncementDto>(data, $"Ogłoszenie o numerze id {id}"));
        }

        [Cached(2)]
        [HttpGet("getAllFromCategory/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<IEnumerable<AnnouncementDto>>))]
        public async Task<IActionResult> GetAllFromCategory(int id)
        {
            var data = _mapper
                .Map<IEnumerable<Announcement>, IEnumerable<AnnouncementDto>>(await _service.GetAllFromCategory(id));
            return Ok(new BaseResponse<IEnumerable<AnnouncementDto>>(data, $"Lista ogłoszeń z kateogrii o id {id}"));
        }

        [Cached(2)]
        [HttpGet("getFavouritesFromCategory/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<IEnumerable<AnnouncementDto>>))]
        public async Task<IActionResult> GetFavouritesFromCategory(int id)
        {
            var data = _mapper
                .Map<IEnumerable<Announcement>, IEnumerable<AnnouncementDto>>(await _service.GetFavouritesFromCategory(id));
            return Ok(new BaseResponse<IEnumerable<AnnouncementDto>>(data, $"Lista ulubionych ogłoszeń z kategorii o id {id}"));
        }

        [Cached(2)]
        [HttpGet("getAllFavourites")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<IEnumerable<AnnouncementDto>>))]
        public async Task<IActionResult> GetAllFavourites()
        {
            var data = _mapper
                .Map<IEnumerable<Announcement>, IEnumerable<AnnouncementDto>>(await _service.GetAllFavourites());
            return Ok(new BaseResponse<IEnumerable<AnnouncementDto>>(data, $"Lista wszystkich ulubionych ogłoszeń"));
        }

        [HttpPut("addToFavourites/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<string>))]
        public async Task<IActionResult> AddToFavourites(int id)
        {
            _cache.Clear();
            var result = await _service.AddToFavourites(id);
            if (result == null)
                throw new ApiException("Coś poszło nie tak. Spróbuj ponownie.") { StatusCode = (int)HttpStatusCode.BadRequest };
            return Ok(new BaseResponse<string>("Ogłoszenie zostało dodane do ulubionych"));
        }

        [HttpPut("removeFromFavourites/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<string>))]
        public async Task<IActionResult> RemoveFromFavourites(int id)
        {
            _cache.Clear();
            var result = await _service.RemoveFromFavourites(id);
            if (result == null)
                throw new ApiException("Coś poszło nie tak. Spróbuj ponownie.") { StatusCode = (int)HttpStatusCode.BadRequest };
            return Ok(new BaseResponse<string>("Ogłoszenie zostało usunięte z ulubionych"));
        }

        [HttpPut("markAsReaded/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<string>))]
        public async Task<IActionResult> MarkAsReaded(int id)
        {
            _cache.Clear();
            var result = await _service.MarkAsReaded(id);
            if (result == null)
                throw new ApiException("Coś poszło nie tak. Spróbuj ponownie.") { StatusCode = (int)HttpStatusCode.BadRequest };
            return Ok(new BaseResponse<string>("Ogłoszenie zostało oznaczone jako przeczytane"));
        }

        [HttpPut("markManyAsReaded")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<string>))]
        public async Task<IActionResult> MarkManyAsReaded([FromBody] AnnouncementMarkAsReadedRequest request)
        {
            _cache.Clear();
            var result = await _service.MarkManyAsReaded(request.AnnouncementsToMarkAsReaded);
            if (!result)
                throw new ApiException("Coś poszło nie tak. Spróbuj ponownie.") { StatusCode = (int)HttpStatusCode.BadRequest };
            return Ok(new BaseResponse<string>("Ogłoszenia zostały oznaczone jako przeczytane"));
        }

        [HttpPut("markAsUnreaded/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<string>))]
        public async Task<IActionResult> MarkAsUnreaded(int id)
        {
            _cache.Clear();
            var result = await _service.MarkAsUnreaded(id);
            if (result == null)
                throw new ApiException("Coś poszło nie tak. Spróbuj ponownie.") { StatusCode = (int)HttpStatusCode.BadRequest };
            return Ok(new BaseResponse<string>("Ogłoszenie zostało oznaczone jako nieprzeczytane"));
        }

        [HttpPut("markManyAsUnreaded")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<string>))]
        public async Task<IActionResult> MarkManyAsUnreaded([FromBody] AnnouncementMarkAsReadedRequest request)
        {
            _cache.Clear();
            var result = await _service.MarkManyAsUnreaded(request.AnnouncementsToMarkAsReaded);
            if (!result)
                throw new ApiException("Coś poszło nie tak. Spróbuj ponownie.") { StatusCode = (int)HttpStatusCode.BadRequest };
            return Ok(new BaseResponse<string>("Ogłoszenia zostały oznaczone jako nieprzeczytane"));
        }
    }
}
