using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TuyenDung.Data.DataContext;
using TuyenDung.Data.Dto;
using TuyenDung.Data.Model;
using TuyenDung.Service.Services.InterfaceIServices;

namespace TuyenDung.Service.Services.RepositoryServices
{
    public class EmployersService : IEmployersIService
    {
        private readonly MyDb _dbContext;
        public EmployersService(MyDb dbContext)
        {
            _dbContext = dbContext;
        }
        public Employers Create(EmployersDto employersDto, IFormFile image)
        {
            try
            {
                if(employersDto == null)
                {
                    throw new ArgumentNullException(nameof(employersDto), "All data fields have not been filled in");
                }
                Employers employers = new Employers
                {
                    Name = employersDto.Name,
                    Industry = employersDto.Industry,
                    Website = employersDto.Website,
                    Address = employersDto.Address,
                    ContactName = employersDto.ContactName,
                    ContactPosition = employersDto.ContactPosition,
                    ContactEmail = employersDto.ContactEmail,
                    ContactPhone = employersDto.ContactPhone,
                };
                if(image != null && image.Length > 0)
                {
                    string imagePath = SaveProductImage(image);
                    employers.Image = imagePath;
                    _dbContext.Employers.Add(employers);
                    _dbContext.SaveChanges();
                }
                return employers;
            }
            catch (Exception ex)
            {
                throw new Exception("There is an error when creating a Produc", ex);
            }
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Employers Update(Employers employers)
        {
            throw new NotImplementedException();
        }

        private string SaveProductImage(IFormFile image)
        {
            try
            {
                string currentDateFolder = DateTime.Now.ToString("dd-MM-yyyy");
                string imagesFolder = Path.Combine(@"C:\Users\XuanThai\Desktop\ImageXedap", "Prodycts_images", currentDateFolder);
                if (!Directory.Exists(imagesFolder))
                {
                    Directory.CreateDirectory(imagesFolder);
                }
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                string filePath = Path.Combine(imagesFolder, fileName);

                using(var stream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }
                return filePath;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while saving the image: {ex.Message}");
            }
        }
    }
}
