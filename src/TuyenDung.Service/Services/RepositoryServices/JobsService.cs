using Microsoft.Extensions.Caching.Distributed;
using System.Linq;
using System.Text.Json;
using TuyenDung.Data.DataContext;
using TuyenDung.Data.Dto;
using TuyenDung.Data.Model;
using TuyenDung.Data.Model.Enum;
using TuyenDung.Service.Repository.Interface;
using TuyenDung.Service.Services.InterfaceIServices;

namespace TuyenDung.Service.Services.RepositoryServices
{
    public class JobsService : IJobsIService
    {
        private readonly MyDb _dbContext;
        private readonly IJobsInterface _iJobsInterface;
        private readonly IDistributedCache _distributedCache;
        private readonly IEmployersInterface _employersInterface;
        public JobsService(MyDb dbContext, IJobsInterface iJobsInterface, IDistributedCache distributedCache, IEmployersInterface employersInterface)
        {
            _distributedCache = distributedCache;
            _dbContext = dbContext;
            _iJobsInterface = iJobsInterface;
            _employersInterface = employersInterface;
        }
        public Jobs Create(JobsDto jobsDto)
        {
            try
            {
                if (jobsDto == null)
                {
                    throw new ArgumentNullException(nameof(jobsDto), "All data fields have not been filled in");
                }
                var employers = _employersInterface.GetById(jobsDto.EmployerId);
                if (employers == null)
                {
                    throw new Exception("Employer not found");
                }

                // Generate a unique ID for the job
                int jobId = GenerateUniqueJobId();

                Jobs jobs = new Jobs
                {
                    Id = jobId, // Assign the generated ID
                    EmployerId = employers.Id,
                    Name = jobsDto.Name,
                    Title = jobsDto.Title,
                    Description = jobsDto.Description,
                    Requirements = jobsDto.Requirements,
                    Salary = jobsDto.Salary,
                    Location = jobsDto.Location,
                    JobType = StatusJobType.Full_time,
                    Status = StatusJobs.Open,
                    PostedDate = DateTime.Now,
                    Deadline = jobsDto.Deadline
                };

                var serializedJob = JsonSerializer.Serialize(jobs);
                var cacheKey = $"Job_{jobs.Id}";

                // Save the job to the cache
                _distributedCache.SetString(cacheKey, serializedJob);

                return jobs;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the job.", ex);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                // Tạo khóa Redis cho công việc cần xóa
                var cachekey = $"Job_{id}";
                //Kiem tra jobs co ton tai hay k
                var job = _distributedCache.Get(cachekey);
                if (job == null)
                {
                    throw new Exception("Id not found in Redis");
                }
                _distributedCache.Remove(cachekey);
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception("An error occurred while delete jobs in.", ex);
            }
        }

        public Jobs Update(JobsUpdateDto jobsUpdateDto)
        {
            try
            {
                var cacheKey = $"Job_{jobsUpdateDto.Id}";
                //lay cong viec tu Redis
                var jobsJson = _distributedCache.GetString(cacheKey);
                if(jobsJson == null)
                {
                    throw new Exception("jobsId not found in Redis");
                }
                //giai tuan tu doi tuong Jobs tu Json
                var jobs = JsonSerializer.Deserialize<Jobs>(jobsJson);

                jobs.Title = jobsUpdateDto.Title;
                jobs.Description = jobsUpdateDto.Description;
                jobs.Requirements = jobsUpdateDto.Requirements;
                jobs.Salary = jobsUpdateDto.Salary;
                jobs.Location = jobsUpdateDto.Location;
                jobs.JobType = jobsUpdateDto.JobType;
                jobs.Status = jobsUpdateDto.Status;
                jobs.Deadline = jobsUpdateDto.Deadline;

                //Tuan tu hoa lai doi tuong Jobs
                var updatejobsJson = _distributedCache.GetString(cacheKey);
                _distributedCache.SetString(cacheKey, jobsJson);
                return jobs;
            }
            catch (Exception ex) 
            {
                throw new Exception("An error occurred while update jobs in.", ex);
            }
        }
        private int GenerateUniqueJobId()
        {
            return 1;
        }
    }
}
