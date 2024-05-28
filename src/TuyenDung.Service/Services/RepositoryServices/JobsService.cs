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
        public JobsService(MyDb dbContext, IJobsInterface iJobsInterface)
        {
            _dbContext = dbContext;
            _iJobsInterface = iJobsInterface;
        }
        public Jobs Create(JobsDto jobsDto)
        {
            try
            {
                if(jobsDto == null)
                {
                    throw new ArgumentNullException(nameof(jobsDto), "All data fields have not been filled in");
                }
                var employers = _dbContext.Jobs.FirstOrDefault(x => x.Id == jobsDto.EmployerId);
                if (employers == null)
                {
                    throw new Exception("employersId not found");
                }
                Jobs jobs = new Jobs
                {
                    EmployerId = employers.EmployerId,
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
                _dbContext.Jobs.Add(jobs);
                _dbContext.SaveChanges();
                return jobs;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while create jobs in.", ex);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var jobs = _iJobsInterface.GetById(id);
                if(jobs == null)
                {
                    throw new Exception("Id not found");
                }
                _dbContext.Remove(jobs);
                _dbContext.SaveChanges();
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
                var jobsId = _iJobsInterface.GetById(jobsUpdateDto.Id);
                if(jobsId == null)
                {
                    throw new Exception("jobsId not found");
                }
                jobsId.Title = jobsUpdateDto.Title;
                jobsId.Description = jobsUpdateDto.Description;
                jobsId.Requirements = jobsUpdateDto.Requirements;
                jobsId.Salary = jobsUpdateDto.Salary;
                jobsId.Location = jobsUpdateDto.Location;
                jobsId.JobType = jobsUpdateDto.JobType;
                jobsId.Status = jobsUpdateDto.Status;
                jobsId.Deadline = jobsUpdateDto.Deadline;
                _dbContext.Jobs.Update(jobsId);
                _dbContext.SaveChanges();
                return jobsId;
            }
            catch (Exception ex) 
            {
                throw new Exception("An error occurred while update jobs in.", ex);
            }
        }
    }
}
