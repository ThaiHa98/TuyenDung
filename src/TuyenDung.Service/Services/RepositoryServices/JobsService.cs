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
        private readonly IEmployersInterface _employersInterface;

        public JobsService(MyDb dbContext, IEmployersInterface employersInterface)
        {
            _dbContext = dbContext;
            _employersInterface = employersInterface;
        }

        public Jobs Create(JobsDto jobsDto)
        {
            try
            {
                var employerId = _dbContext.Employers.FirstOrDefault(x => x.Id == jobsDto.EmployerId);
                if (employerId == null)
                {
                    throw new Exception("employerId not found");
                }
                Jobs jobs = new Jobs
                {
                    EmployerId = employerId.Id,
                    Name = jobsDto.Name,
                    Title = jobsDto.Title,
                    Description = jobsDto.Description,
                    Requirements = jobsDto.Requirements,
                    Salary = jobsDto.Salary,
                    Location = jobsDto.Location,
                    Deadline = jobsDto.Deadline,
                    PostedDate = DateTime.Now,
                    JobType = StatusJobType.Full_time,
                    Status = StatusJobs.Open,
                };
                _dbContext.Jobs.Add(jobs);
                _dbContext.SaveChanges();
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
                var jobFromDb = _dbContext.Jobs.FirstOrDefault(j => j.Id == id);
                if (jobFromDb == null)
                {
                    throw new Exception($"Job with ID {id} not found in the database.");
                }
                _dbContext.Jobs.Remove(jobFromDb);
                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the job.", ex);
            }
        }

        public Jobs Update(JobsUpdateDto jobsUpdateDto)
        {
            try
            {
                var jobFromDb = _dbContext.Jobs.FirstOrDefault(j => j.Id == jobsUpdateDto.Id);
                if (jobFromDb == null)
                {
                    throw new Exception($"Job with ID {jobsUpdateDto.Id} not found in the database.");
                }
                jobFromDb.Title = jobsUpdateDto.Title;
                jobFromDb.Description = jobsUpdateDto.Description;
                jobFromDb.Requirements = jobsUpdateDto.Requirements;
                jobFromDb.Salary = jobsUpdateDto.Salary;
                jobFromDb.Location = jobsUpdateDto.Location;
                jobFromDb.JobType = jobsUpdateDto.JobType;
                jobFromDb.Status = jobsUpdateDto.Status;
                jobFromDb.Deadline = jobsUpdateDto.Deadline;

                _dbContext.SaveChanges();

                return jobFromDb;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the job.", ex);
            }
        }
    }
}
