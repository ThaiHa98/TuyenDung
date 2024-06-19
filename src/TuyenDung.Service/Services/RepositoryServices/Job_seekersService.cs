using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient.Server;
using TuyenDung.Data.DataContext;
using TuyenDung.Data.Dto;
using TuyenDung.Data.Model;
using TuyenDung.Data.Model.Enum;
using TuyenDung.Service.Repository.Interface;
using TuyenDung.Service.Services.InterfaceIServices;

namespace TuyenDung.Service.Services.RepositoryServices
{
    public class Job_seekersService : IJob_seekersIService
    {
        private readonly MyDb _dbContext;
        private readonly IFormCvInterface _formCvInterface;
        public Job_seekersService(MyDb dbContext,IFormCvInterface formCvInterface)
        {
            _formCvInterface = formCvInterface;
            _dbContext = dbContext;
        }
        public Job_seekers Create(Job_seekersDto1 jobSeekersDto1, IFormFile image, IFormFile formFile)
        {
            try
            {
                if (jobSeekersDto1 == null)
                {
                    throw new ArgumentNullException(nameof(jobSeekersDto1), "Chưa điền đầy đủ thông tin vào các trường dữ liệu");
                }
                var user = _dbContext.Users.FirstOrDefault(x => x.Id == jobSeekersDto1.UserId_Job_seekers);
                if (user == null)
                {
                    throw new Exception("User không tồn tại.");
                }

                Job_seekers jobSeekersService = new Job_seekers
                {
                    UserId = user.Id,
                    User_Name = user.Name,
                    DateofBirth = jobSeekersDto1.DateofBirth_Job_seekers,
                    Gender = jobSeekersDto1.Gender_Job_seekers,
                    DateCreate = DateTime.Now
                };

                if (image != null && image.Length > 0)
                {
                    string imagePath = SaveEmployersImage(image);
                    jobSeekersService.Image = imagePath;
                }

                _dbContext.Job_Seekers.Add(jobSeekersService);
                _dbContext.SaveChanges();

                Applications applications = new Applications
                {
                    Job_JobId = jobSeekersService.Id,
                    User_UserId = jobSeekersService.UserId,
                    Status = StatusApplication.Pending,
                    StatusSubmissionType = StatusSubmissionType.Online,
                    Date = DateTime.Now,
                };

                if (formFile != null && formFile.Length > 0)
                {
                    string filePath = SaveFile(formFile);
                    applications.CvFilePath = filePath;
                }
                else
                {
                    List<FormCv> availableCvTemplates = _formCvInterface.GetFormCv();
                    if (availableCvTemplates.Count > 0)
                    {
                        applications.CvFilePath = availableCvTemplates[0].CvFilePath;
                    }
                    else
                    {
                        throw new Exception("Không có mẫu CV nào có sẵn.");
                    }
                }

                _dbContext.Applications.Add(applications);
                _dbContext.SaveChanges();

                return jobSeekersService;
            }
            catch (Exception ex)
            {
                throw new Exception($"Đã xảy ra lỗi khi tạo Job_seekers: {ex.Message}");
            }
        }


        public bool Delete(int job_seekersid)
        {
            try
            {
                var job_seekers = _dbContext.Job_Seekers.FirstOrDefault(x => x.Id == job_seekersid);
                if(job_seekers == null)
                {
                    throw new Exception("job_seekersId not found");
                }
                _dbContext.Remove(job_seekers);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting Job_seekers: {ex.Message}");
            }
        }

        public Job_seekers Update(Job_seekersDto job_seekersDto, IFormFile image)
        {
            try
            {
                if (job_seekersDto == null)
                {
                    throw new ArgumentNullException(nameof(job_seekersDto), "All data fields have not been filled in");
                }
                var job_seekers = _dbContext.Job_Seekers.FirstOrDefault(x => x.Id == job_seekersDto.Id);
                if (job_seekers == null)
                {
                    throw new Exception("job_seekersId not found");
                }
                job_seekers.UserId = job_seekersDto.UserId_Job_seekers;
                job_seekers.User_Name = job_seekersDto.Name_Job_seekers;
                job_seekers.DateofBirth = job_seekersDto.DateofBirth_Job_seekers;
                job_seekers.Gender = job_seekersDto.Gender_Job_seekers;
                job_seekers.DateCreate = DateTime.Now;
                _dbContext.Job_Seekers.Update(job_seekers);
                _dbContext.SaveChanges();
                return job_seekers;
            }
            catch(Exception ex)
            {
                throw new Exception($"An error occurred while updating Job_seekers: {ex.Message}");
            }
        }
        private string SaveEmployersImage(IFormFile image)
        {
            try
            {
                string currentDateFolder = DateTime.Now.ToString("dd-MM-yyyy");
                string imagesFolder = Path.Combine(@"C:\Users\Xuanthai98\OneDrive\Máy tính\ImageSlide", "Prodycts_images", currentDateFolder);
                if (!Directory.Exists(imagesFolder))
                {
                    Directory.CreateDirectory(imagesFolder);
                }
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                string filePath = Path.Combine(imagesFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
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
        private string SaveFile(IFormFile file)
        {
            try
            {
                // Định nghĩa các phần mở rộng tệp hợp lệ
                var allowedExtensions = new[] { ".docx", ".pptx", ".pdf", ".xlsx", ".txt" };

                // Lấy phần mở rộng của tệp và chuyển thành chữ thường để so sánh không phân biệt chữ hoa chữ thường
                var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    throw new Exception("Định dạng tệp không hợp lệ.");
                }
                string filesFolder = Path.Combine(@"C:\Users\Xuanthai98\OneDrive\Máy tính\ImageSlide\CV");
                if (!Directory.Exists(filesFolder))
                {
                    Directory.CreateDirectory(filesFolder);
                }
                string fileName = Guid.NewGuid().ToString() + fileExtension;
                string filePath = Path.Combine(filesFolder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return filePath;
            }
            catch (Exception ex)
            {
                throw new Exception($"Đã xảy ra lỗi khi lưu tệp: {ex.Message}");
            }
        }
    }
}
