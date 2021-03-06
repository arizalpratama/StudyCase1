using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnrollmentService.Data;
using EnrollmentService.Models;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using EnrollmentService.SyncDataService.Http;
using EnrollmentService.Dtos;


namespace EnrollmentService.Controllers
{
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
        [Authorize(Roles = "admin, student")]
        [HttpGet]
        public async Task<IEnumerable<Enrollment>> GetAllEnrollments()
        {
            Console.WriteLine("--> Getting Enrollments .....");
            var enrollmentItem = await _enrollment.GetAllEnrollments();
            return enrollmentItem;
        }

        //Get By Id
        [Authorize(Roles = "admin, student")]
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
        [Authorize(Roles = "admin, student")]
        [HttpPost]   
        public async Task<ActionResult<EnrollmentReadDto>> CreateEnrollment(EnrollmentCreateDto enrollmentCreateDto)
        {
            try
            {
                var enrollmentModel = _mapper.Map<Enrollment>(enrollmentCreateDto);
                _enrollment.CreateEnrollment(enrollmentModel);
                _enrollment.SaveChanges();

                var enrollmentReadDto = _mapper.Map<EnrollmentReadDto>(enrollmentModel);

                if (enrollmentReadDto != null)
                {
                    //send sync communication
                    try
                    {
                        await _paymentDataClient.SendEnrollmentToPayment(enrollmentReadDto);
                        return Ok(enrollmentReadDto);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"--> Could Not Send Synchronously: {ex.Message}");
                    }
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Update
        [Authorize(Roles = "admin, student")]
        [HttpPut("{id}")]
        public ActionResult<EnrollmentReadDto> UpdateEnrollment(int id, EnrollmentCreateDto enrollmentCreateDto)
        {
            try
            {
                var enrollmenteModel = _mapper.Map<Enrollment>(enrollmentCreateDto);
                _enrollment.UpdateEnrollment(id, enrollmenteModel);
                _enrollment.SaveChanges();

                if (enrollmentCreateDto != null)
                {
                    return Ok(enrollmentCreateDto);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Delete
        [Authorize(Roles = "admin, student")]
        [HttpDelete("{id}", Name = "DeleteEnrollment")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _enrollment.DeleteEnrollment(id.ToString());
                return Ok($"Data enrollment {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}