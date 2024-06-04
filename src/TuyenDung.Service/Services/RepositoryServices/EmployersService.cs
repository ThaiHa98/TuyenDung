using Microsoft.AspNetCore.Http;
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
                    Name = employersDto.NameEmployers,
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
                    string imagePath = SaveEmployersImage(image);
                    employers.Image = imagePath;
                    _dbContext.Employers.Add(employers);
                    _dbContext.SaveChanges();
                }
                return employers;
            }
            catch (Exception ex)
            {
                throw new Exception("There is an error when creating a employers", ex);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var employers = _dbContext.Employers.FirstOrDefault(x => x.Id == id);
                if(employers == null)
                {
                    throw new Exception("employersId not found");
                }
                _dbContext.Remove(employers);
                _dbContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception("There was an error deleteting the employer");
            }
        }

        public Employers Update(EmployersDto employersDto, IFormFile image)
        {
            try
            {
                var employersDtoId = _dbContext.Employers.FirstOrDefault(x => x.Id == employersDto.Id);
                if(employersDtoId == null)
                {
                    throw new Exception("employersDtoId not found");
                }
                employersDtoId.Id = employersDto.Id;
                employersDtoId.Name = employersDto.NameEmployers;
                employersDtoId.Industry = employersDto.Industry;
                employersDtoId.Website = employersDto.Website;
                employersDtoId.Address = employersDto.Address;
                employersDtoId.ContactName = employersDto.ContactName;
                employersDtoId.ContactPosition = employersDto.ContactPosition;
                employersDtoId.ContactEmail = employersDto.ContactEmail;
                employersDtoId.ContactPhone = employersDto.ContactPhone;
                if(image !=  null && image.Length > 0)
                {
                    string imagePath = SaveEmployersImage(image);
                    employersDto.Image = imagePath;
                    _dbContext.Employers.Add(employersDtoId);
                    _dbContext.SaveChanges();
                }
                return employersDtoId;
            }
            catch(Exception ex)
            {
                throw new Exception("There was an error updating the employer");
            }
        }

        private string SaveEmployersImage(IFormFile image)
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
