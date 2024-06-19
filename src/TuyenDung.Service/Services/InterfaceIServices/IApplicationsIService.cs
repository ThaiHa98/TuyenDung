using Microsoft.AspNetCore.Http;
using TuyenDung.Data.Dto;
using TuyenDung.Data.Model;

namespace TuyenDung.Service.Services.InterfaceIServices
{
    public interface IApplicationsIService
    {
        public Applications Create(ApplicationsDto applicationsDto, IFormFile formFile);
        string Update(int Id, IFormFile formFile);
        bool Delete(int Id);


    }
}
