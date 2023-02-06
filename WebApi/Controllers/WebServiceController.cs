using AutoMapper;
using Caching.Interfaces;
using Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DbEntities;
using Models.DTOs;
using Models.DTOs.WebService;
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
    public class WebServiceController : ControllerBase
    {
        private readonly IWebServiceService _service;
        private readonly IMapper _mapper;
        private readonly ICacheManager _cache;

        public WebServiceController(IWebServiceService webServiceService, IMapper mapper, ICacheManager cache)
        {
            _service = webServiceService;
            _mapper = mapper;
            _cache = cache;
        }

        [Cached(2)]
        [HttpGet("getAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<IEnumerable<WebServiceDto>>))]
        public async Task<IActionResult> GetAll()
        {
            var data = _mapper
                .Map<IEnumerable<WebService>, IEnumerable<WebServiceDto>>(await _service.GetAll());
            return Ok(new BaseResponse<IEnumerable<WebServiceDto>>(data, $"Lista serwisów"));
        }

        [HttpGet("getOne/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<WebServiceDto>))]
        public async Task<IActionResult> GetOne(int id)
        {
            var data = _mapper
                .Map<WebService, WebServiceDto>(await _service.Get(id));
            if (data == null)
                throw new ApiException("Serwis nie istnieje") { StatusCode = (int)HttpStatusCode.BadRequest };
            return Ok(new BaseResponse<WebServiceDto>(data, $"Serwis o numerze id {id}"));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<string>))]
        public async Task<IActionResult> Add([FromBody] WebServiceRequest request)
        {
            _cache.Clear();
            var result = await _service.Add(_mapper.Map<WebServiceRequest, WebService>(request));
            if (result == null)
                throw new ApiException("Coś poszło nie tak. Spróbuj ponownie.") { StatusCode = (int)HttpStatusCode.BadRequest };
            return Ok(new BaseResponse<string>("Serwis został dodany"));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<string>))]
        public async Task<IActionResult> Update([FromBody] WebServiceUpdateRequest request)
        {
            _cache.Clear();
            var result = await _service.Update(request);
            if (result == null)
                throw new ApiException("Coś poszło nie tak. Spróbuj ponownie.") { StatusCode = (int)HttpStatusCode.BadRequest };
            return Ok(new BaseResponse<string>("Serwis został zaaktualizowany"));
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<string>))]
        public async Task<IActionResult> Delete(int id)
        {
            _cache.Clear();
            var result = await _service.Delete(id);
            if (!result)
                throw new ApiException("Coś poszło nie tak. Spróbuj ponownie.") { StatusCode = (int)HttpStatusCode.BadRequest };
            return Ok(new BaseResponse<string>("Serwis został usunięty"));
        }
    }
}
