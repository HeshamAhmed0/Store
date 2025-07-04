using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Presentation
{
    [ApiController]
    [Route("apis/[controller]")]
    public class BuggyController:ControllerBase
    {
        [HttpGet("Notfound")]
        public IActionResult GetNotFoundRequest()
        {
            return NotFound();
        }

        [HttpGet("ServerError")]
        public IActionResult GetServerErrorRequest()
        {
            throw new Exception();
            return Ok();
        }
        [HttpGet("BadRequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest();
        }
        [HttpGet("ValidationError/{id}")]
        public IActionResult GetValidationErrorRequest(int id)
        {
            return BadRequest();
        }
        [HttpGet("UnAuthorized")]
        public IActionResult GetUnAuthorizedRequest()
        {
            return Unauthorized();
        }
    }
}
