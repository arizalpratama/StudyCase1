using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using EnrollmentService.Data;
using EnrollmentService.Models;
using EnrollmentService.SyncDataServices.Http;
using EnrollmentService.Interface;
using EnrollmentService.Dtos;

namespace EnrollmentService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpGet]
        public ActionResult<IEnumerable<EnrollmentDto>> GetEnrollments()
        {
            Console.WriteLine("--> Getting Platforms .....");
            var enrollmentItem = _repository.GetAllEnrollments();
            return Ok(_mapper.Map<IEnumerable<EnrollmentDto>>(enrollmentItem));
        }

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

        [HttpPost]
        public async Task<ActionResult<EnrollmentDto>> CreateEnrollment(EnrollmentForCreateDto enrollmentCreateDto)
        {
            var enrollmentModel = _mapper.Map<Enrollment>(enrollmentCreateDto);
            _repository.CreateEnrollment(enrollmentModel);
            _repository.SaveChanges();

            var enrollmentDto = _mapper.Map<EnrollmentDto>(enrollmentModel);


            //send sync
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
    }
}