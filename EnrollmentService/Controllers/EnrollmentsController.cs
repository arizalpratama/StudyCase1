using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnrollmentService.Data;
using EnrollmentService.Models;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using EnrollmentService.SyncDataService.Http;
using EnrollmentService.Dtos;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnrollmentService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollment _enrollment;
        private IMapper _mapper;
        private readonly IPaymentDataClient _paymentDataClient;

        public EnrollmentsController(IEnrollment enrollment,
        IMapper mapper, IPaymentDataClient paymentDataClient)
        {
            _enrollment = enrollment;
            _mapper = mapper;
            _paymentDataClient = paymentDataClient;
        }

        // GET: api/<EnrollmentsController>
        [HttpGet]
        public async Task<IEnumerable<Enrollment>> GetAllEnrollments()
        {
            //var results = await _enrollment.GetAll();
            //return results;
            Console.WriteLine("--> Getting Enrollments .....");
            var enrollmentItem = await _enrollment.GetAllEnrollments();
            return enrollmentItem;
            //return Ok(_mapper.Map<IEnumerable<EnrollmentReadDto>>(enrollmentItem));
        }

        // GET api/<EnrollmentsController>/5
        [HttpGet("{id}", Name = "GetEnrollmentById")]
        public async Task<ActionResult<Enrollment>> GetEnrollmentById(string id)
        {
            var enrollmentItem = await _enrollment.GetEnrollmentById(id);
            if (enrollmentItem != null)
            {
                return enrollmentItem;
            }
            return NotFound();
        }

        // POST api/<EnrollmentsController>
        [HttpPost]
        public async Task<ActionResult<EnrollmentReadDto>> CreateEnrollment(EnrollmentCreateDto enrollmentCreateDto)
        {
            var enrollmentModel = _mapper.Map<Enrollment>(enrollmentCreateDto);
            await _enrollment.CreateEnrollment(enrollmentModel);
            _enrollment.SaveChanges();

            var enrollmentReadDto = _mapper.Map<EnrollmentReadDto>(enrollmentModel);


            //send sync
            try
            {
                await _paymentDataClient.SendEnrollmentToPayment(enrollmentReadDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send synchronously: {ex.Message}");
            }

            return CreatedAtRoute(nameof(GetEnrollmentById),
            new { Id = enrollmentReadDto.EnrollmentId }, enrollmentReadDto);
        }

        // PUT api/<EnrollmentsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<EnrollmentsController>/5
        [HttpDelete("{id}", Name = "DeleteEnrollment")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _enrollment.DeleteEnrollment(id.ToString());
                return Ok($"Data student {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

/*using AutoMapper;
using EnrollmentService.Dtos;
using EnrollmentService.Interface;
using EnrollmentService.SyncDataServices.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnrollmentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollment _repository;
        private IMapper _mapper;
        private readonly IPaymentDataClient _paymentDataClient;

        public EnrollmentsController(IEnrollment repository,
        IMapper mapper, IPaymentDataClient paymentDataClient)
        {
            _repository = repository;
            _mapper = mapper;
            _paymentDataClient = paymentDataClient;
        }

        //Get All
        [Authorize(Roles = "admin,student")]
        [HttpGet]
        public ActionResult<IEnumerable<EnrollmentDto>> GetEnrollments()
        {
            Console.WriteLine("--> Getting Enrollments .....");
            var enrollmentItem = _repository.GetAllEnrollments();
            return Ok(_mapper.Map<IEnumerable<EnrollmentDto>>(enrollmentItem));
        }

        //Get By Id
        [Authorize(Roles = "admin,student")]
        [HttpGet("{id}", Name = "GetEnrollmentById")]
        public ActionResult<EnrollmentDto> GetEnrollmentById(int id)
        {
            var enrollmentItem = _repository.GetEnrollmentById(id);
            if (enrollmentItem != null)
            {
                return Ok(_mapper.Map<EnrollmentDto>(enrollmentItem));
            }
            return NotFound();
        }

        //Create
        [Authorize(Roles = "admin,student")]
        [HttpPost]
        public async Task<ActionResult<EnrollmentDto>> CreateEnrollment(EnrollmentForCreateDto enrollmentForCreateDto)
        {
            var enrollmentModel = _mapper.Map<Models.Enrollment>(enrollmentForCreateDto);
            _repository.CreateEnrollment(enrollmentModel);
            _repository.SaveChanges();

            var enrollmentDto = _mapper.Map<EnrollmentDto>(enrollmentModel);

            //Send with syncronhus communication
            try
            {
                await _paymentDataClient.SendEnrollmentToPayment(enrollmentDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send synchronously: {ex.Message}");
            }

            return CreatedAtRoute(nameof(GetEnrollmentById),
            new { Id = enrollmentDto.EnrollmentId }, enrollmentDto);
        }

        //Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _repository.Delete(id.ToString());
                return Ok($"Data student {id} berhasil di delete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}*/