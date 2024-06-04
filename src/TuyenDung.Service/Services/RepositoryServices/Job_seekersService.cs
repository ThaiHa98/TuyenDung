using Microsoft.AspNetCore.Http;
using TuyenDung.Data.DataContext;
using TuyenDung.Data.Dto;
using TuyenDung.Data.Model;
using TuyenDung.Service.Services.InterfaceIServices;

namespace TuyenDung.Service.Services.RepositoryServices
{
    public class Job_seekersService : IJob_seekersIService
    {
        private readonly MyDb _dbContext;
        public Job_seekersService(MyDb dbContext)
        {
            _dbContext = dbContext;
        }
        public Job_seekers Create(Job_seekersDto job_seekersDto, IFormFile image)
        {
            try
            {
                if(job_seekersDto == null)
                {
                    throw new ArgumentNullException(nameof(job_seekersDto), "All data fields have not been filled in");
                }
                Job_seekers job_SeekersService = new Job_seekers
                {
                    UserId = job_seekersDto.UserId_Job_seekers,
                    Name = job_seekersDto.Name_Job_seekers,
                    DateofBirth = job_seekersDto.DateofBirth_Job_seekers,
                    Gender = job_seekersDto.Gender_Job_seekers
                };
                _dbContext.Job_Seekers.Add(job_SeekersService);
                _dbContext.SaveChanges();
                return job_SeekersService;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while creating Job_seekers: {ex.Message}");
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
                job_seekers.Name = job_seekersDto.Name_Job_seekers;
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
    }
}
