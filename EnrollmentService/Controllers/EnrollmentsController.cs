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

        //Get All
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

        //Get By Id
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

        //Create
        [HttpPost]
        public async Task<ActionResult<EnrollmentReadDto>> CreateEnrollment(EnrollmentCreateDto enrollmentCreateDto)
        {
            var enrollmentModel = _mapper.Map<Enrollment>(enrollmentCreateDto);
            await _enrollment.CreateEnrollment(enrollmentModel);
            _enrollment.SaveChanges();

            var enrollmentReadDto = _mapper.Map<EnrollmentReadDto>(enrollmentModel);


            //send sync communication
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

        //Delete
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