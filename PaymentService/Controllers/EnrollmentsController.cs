using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace PaymentService.Controllers
{
    [Route("api/p/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        public EnrollmentsController()
        {

        }

        [HttpPost]
        public ActionResult TestIndboundConnection()
        {
            Console.WriteLine("--> Inbound POST command services");
            return Ok("Inbound test from platforms controller");
        }
    }
}
