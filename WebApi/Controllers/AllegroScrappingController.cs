using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DbEntities;
using Models.DTOs.AllegroTokens;
using Models.ResponseModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllegroScrappingController : ControllerBase
    {
        private readonly HttpClient _client;
        private readonly IAllegroTokenService _service;
        private readonly IMapper _mapper;

        public AllegroScrappingController(IAllegroTokenService service, IMapper mapper)
        {
            _client = new HttpClient();
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("allegro/authorize/")]
        public async Task<IActionResult> AllegroAuthorize(string code)
        {
            var values = new Dictionary<string, string>
            {
                ["grant_type"] = "authorization_code",
                ["code"] = code,
                ["redirect_uri"] = "https://appsonda.pl/api/AllegroScrapping/allegro/authorize/"
            };

            var content = new FormUrlEncodedContent(values);

            Serilog.Log.Information("AllegroAuthorizeReqyest: \n" + await content.ReadAsStringAsync());
            var link = "https://allegro.pl/auth/oauth/token";
            var byteArray = Encoding.ASCII.GetBytes("db7a23106bba4df787f85c3957e4d163:Tkcu4x07ZYfYlBaOGPJPPatDrLjvOh1OjPQQu5kK4xT63lEN6SfRBqVlcVB4d2EC");
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var response = await _client.PostAsync(link, content);
            var responseJson = JsonConvert.DeserializeObject<AllegroResponse>(await response.Content.ReadAsStringAsync());
            var allegroResponse = new AllegroToken()
            {
                AccessToken = responseJson.access_token,
                TokenType = responseJson.token_type,
                RefreshToken = responseJson.refresh_token,
                ExpiresIn = responseJson.expires_in,
                AllegroApi = responseJson.allegro_api,
                Jti = responseJson.jti,
                Scope = responseJson.scope
            };
            await _service.Add(allegroResponse,1);
            Serilog.Log.Information("AllegroAuthorizeResponse: \n" + await response.Content.ReadAsStringAsync());
            return Ok();
        }

        [HttpGet("userAllegroTokenExist/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<bool>))]
        public async Task<IActionResult> TokenExist(int id)
        {
            if(await _service.GetLatest(id)!=null)
            {
                return Ok(new BaseResponse<bool>(true));
            }
            return Ok(new BaseResponse<bool>(false));
        }


        [HttpGet("requestAuthorize")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponse<string>))]
        public async Task<IActionResult> RequestAuthorize()
        {
#if DEBUG
            return Ok(new BaseResponse<string>("https://allegro.pl/auth/oauth/authorize?response_type=code&client_id=db7a23106bba4df787f85c3957e4d163&redirect_uri=https://localhost:5001/api/AllegroScrapping/allegro/authorize/", "Link do autoryzacji"));
#endif
            return Ok(new BaseResponse<string>("https://allegro.pl/auth/oauth/authorize?response_type=code&client_id=db7a23106bba4df787f85c3957e4d163&redirect_uri=https://appsonda.pl/api/AllegroScrapping/allegro/authorize/", "Link do autoryzacji"));
        }
    }
}
