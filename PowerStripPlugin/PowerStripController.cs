using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PowerStripPlugin
{
    [Route("api/[controller]")]
    [ApiController]

    public class PowerStripController: ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async ValueTask<IActionResult> GetAllAsync()
        {
           
            return Ok("Hello!");
        }
    }
}
