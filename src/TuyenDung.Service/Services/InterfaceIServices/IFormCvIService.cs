using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuyenDung.Data.Dto;
using TuyenDung.Data.Model;

namespace TuyenDung.Service.Services.InterfaceIServices
{
    public interface IFormCvIService
    {
        public FormCv Create(FormCvDto formCvDto, IFormFile CvFilePath);
        string Update(FormCvDto formCvDto, IFormFile CvFilePath);
        bool Delete(int Id);
        public string ExtractTextFromDocx(string cvFilePath);
    }
}
