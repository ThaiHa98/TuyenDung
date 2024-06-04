using Microsoft.AspNetCore.Http;
using TuyenDung.Data.Dto;
using TuyenDung.Data.Model;

namespace TuyenDung.Service.Services.InterfaceIServices
{
    public interface IEmployersIService
    {
        public Employers Create(EmployersDto employersDto, IFormFile image);
        Employers Update(EmployersDto employersDto, IFormFile image);
        bool Delete(int id);
    }
}
