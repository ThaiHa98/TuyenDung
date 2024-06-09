using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TuyenDung.API.Common;
using TuyenDung.Data.Dto;
using TuyenDung.Service.Services.InterfaceIServices;

namespace TuyenDung.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class formCvsController : ControllerBase
    {
        private readonly IFormCvIService _formCvIService;
        public formCvsController(IFormCvIService formCvIService)
        {
            _formCvIService = formCvIService;
        }
        [HttpPost("Create")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Create([FromForm]FormCvDto formCvDto, [FromForm]IFormFile CvFilePath)
        {
            try
            {
                if(formCvDto == null ||  CvFilePath == null)
                {
                    return BadRequest("CvFilePath & CvFilePath not found");
                }
                var create = _formCvIService.Create(formCvDto, CvFilePath);
                return Ok(new XBaseResult
                {
                    data = create,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = create.Id,
                    message = "Create Successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new XBaseResult
                {
                    success = false,
                    httpStatusCode= (int)HttpStatusCode.BadRequest,
                    message = ex.Message
                });
            }
        }
        [HttpPut("Update")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Update([FromForm] FormCvDto formCvDto, [FromForm] IFormFile CvFilePath)
        {
            try
            {
                if (formCvDto == null || CvFilePath == null)
                {
                    return BadRequest("CvFilePath & CvFilePath not found");
                }
                var create = _formCvIService.Update(formCvDto, CvFilePath);
                return Ok(new XBaseResult
                {
                    data = create,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    message = "Create Successfully"
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
        [HttpDelete("Delete")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Delete(int Id)
        {
            try
            {
                if(Id == null)
                {
                    return BadRequest("Id not found");
                }
                var delete = _formCvIService.Delete(Id);
                return Ok(new XBaseResult
                {
                    data = delete,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    message = "Delete Successfully"
                });
            }
            catch( Exception ex)
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
