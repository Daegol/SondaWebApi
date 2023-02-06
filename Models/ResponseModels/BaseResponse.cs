using Models.DTOs.AnnouncementCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ResponseModels
{
    public class BaseResponse<T>
    {
        public BaseResponse()
        {
        }
        public BaseResponse(T data, string message = null)
        {
            Message = message;
            Data = data;
        }

        public BaseResponse(T data, ErrorScrapper scrapper, string message = null)
        {
            Message = message;
            Data = data;
            ErrorScrapper = scrapper;
        }
        public BaseResponse(string message)
        {
            Message = message;
        }
        public bool Succeeded;
        public string Message { get; set; }
        public ErrorScrapper ErrorScrapper { get; set; }

        public List<string> Errors;
        public T Data { get; set; }
    }
}
