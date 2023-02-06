using AutoMapper;
using Models.DbEntities;
using Models.DTOs;
using Models.DTOs.Account;
using Models.DTOs.AllegroTokens;
using Models.DTOs.AnnouncementCategory;
using Models.DTOs.Log;
using Models.DTOs.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AnnouncementCategory, AnnouncementCategoryDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.Url, o => o.MapFrom(s => s.Url))
                .ForMember(d => d.Announcements, o => o.MapFrom(s => s.Announcements))
                .ForMember(d => d.WebService, o => o.MapFrom(s => s.WebService));

            CreateMap<AnnouncementCategoryRequest, AnnouncementCategory>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.Url, o => o.MapFrom(s => s.Url))
                .ForMember(d => d.WebServiceId, o => o.MapFrom(s => s.WebServiceId));

            CreateMap<Announcement, AnnouncementDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.Url, o => o.MapFrom(s => s.Url))
                .ForMember(d => d.IsFavourite, o => o.MapFrom(s => s.IsFavourite))
                .ForMember(d => d.IsReaded, o => o.MapFrom(s => s.IsReaded))
                .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
                .ForMember(d => d.Image, o => o.MapFrom(s => s.Image))
                .ForMember(d => d.Price, o => o.MapFrom(s => s.Price));

            CreateMap<WebService, WebServiceDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.Url, o => o.MapFrom(s => s.Url));

            CreateMap<WebServiceRequest, WebService>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.Url, o => o.MapFrom(s => s.Url));

            CreateMap<AllegroResponse, AllegroToken>()
                .ForMember(d => d.AllegroApi, o => o.MapFrom(s => s.allegro_api))
                .ForMember(d => d.AccessToken, o => o.MapFrom(s => s.access_token))
                .ForMember(d => d.TokenType, o => o.MapFrom(s => s.token_type))
                .ForMember(d => d.RefreshToken, o => o.MapFrom(s => s.refresh_token))
                .ForMember(d => d.ExpiresIn, o => o.MapFrom(s => s.expires_in))
                .ForMember(d => d.Scope, o => o.MapFrom(s => s.scope))
                .ForMember(d => d.Jti, o => o.MapFrom(s => s.jti));

        }
    }
}
