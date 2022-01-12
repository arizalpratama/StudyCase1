using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Data;
using PaymentService.Dtos;
using System;
using System.Collections.Generic;

namespace PaymentService.Controllers
{
    [ApiController]
    [Route("api/p/[controller]")]
    public class EnrollmentController : ControllerBase
    {
        private readonly IPaymentRepo _repository;
        private readonly IMapper _mapper;

        public EnrollmentController(IPaymentRepo repository,
        IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<EnrollmentReadDto>> GetEnrollments()
        {
            Console.WriteLine("-->Ambil Enrollments dari PaymentsService");
            var enrollmentItems = _repository.GetAllEnrollments();
            return Ok(_mapper.Map<IEnumerable<EnrollmentReadDto>>(enrollmentItems));
        }

        [HttpPost]
        public ActionResult TestIndboundConnection()
        {
            Console.WriteLine("--> Inbound POST payment services");
            return Ok("Inbound test from enrollments controller");
        }
    }
}