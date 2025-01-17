﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TuyenDung.API.Common;
using TuyenDung.Data.Dto;
using TuyenDung.Service.Repository.Interface;
using TuyenDung.Service.Services.InterfaceIServices;

namespace TuyenDung.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Job_seekersController : ControllerBase
    {
        private readonly IJob_seekersIService _job_SeekersIService;
        private readonly IFormCvInterface _formCvInterface;
        public Job_seekersController(IJob_seekersIService job_SeekersService,IFormCvInterface formCvInterface)
        {
            _formCvInterface = formCvInterface;
            _job_SeekersIService = job_SeekersService;
        }
        [HttpPost("Create")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Create([FromForm] Job_seekersDto1 job_seekersDto1, IFormFile image, [FromForm] IFormFile formFile)
        {
            try
            {
                if (job_seekersDto1 == null)
                {
                    return BadRequest("job_seekersDto1 not found");
                }

                if (image == null)
                {
                    return BadRequest("image not found");
                }

                var createJob_seekers = _job_SeekersIService.Create(job_seekersDto1, image, formFile);

                return Ok(new XBaseResult
                {
                    data = createJob_seekers,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    message = "Create job_seekers Successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new XBaseResult
                {
                    success = false,
                    httpStatusCode = (int)HttpStatusCode.BadRequest,
                    message = ex.Message
                });
            }
        }
        [HttpPut("Update")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Update([FromForm] Job_seekersDto job_seekersDto, [FromForm] IFormFile image)
        {
            try
            {
                if (job_seekersDto == null || image == null)
                {
                    return BadRequest("job_seekersDto & image not found");
                }
                var updatejob_seekers = _job_SeekersIService.Update(job_seekersDto, image);
                return Ok(new XBaseResult
                {
                    data = updatejob_seekers,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    message = "Update job_seekers Successfull"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new XBaseResult
                {
                    success = false,
                    httpStatusCode = (int)HttpStatusCode.BadRequest,
                    message = ex.Message
                });
            }
        }
        [HttpDelete("Delete")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Delete(int id) 
        {
            try
            {
                var delete = _job_SeekersIService.Delete(id);
                return Ok(new XBaseResult
                {
                    data = delete,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    message = "Delet job_seekers Successfully"
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new XBaseResult
                {
                    success = false,
                    httpStatusCode = (int)HttpStatusCode.BadRequest,
                    message = ex.Message
                });
            }
        }
    }
}
