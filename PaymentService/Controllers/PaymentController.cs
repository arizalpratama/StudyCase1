using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Data;
using PaymentService.Dtos;
using PaymentService.Models;
using System;
using System.Collections.Generic;

namespace PaymentService.Controllers
{
    public class PaymentController
    {
        [ApiController]
        [Route("api/c/enrollments/{enrollmentId}/[controller]")]
        public class PaymentsController : ControllerBase
        {
            private readonly IPaymentRepo _repository;
            private readonly IMapper _mapper;
            public PaymentsController(IPaymentRepo repository,
            IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            [HttpGet]
            public ActionResult<IEnumerable<PaymentReadDto>> GetPaymentsForEnrollment(int enrollmentId)
            {
                Console.WriteLine($"--> GetPaymentsForEnrollment: {enrollmentId}");
                if (!_repository.EnrollmentExist(enrollmentId))
                {
                    return NotFound();
                }
                var payments = _repository.GetPaymentsForEnrollment(enrollmentId);
                return Ok(_mapper.Map<IEnumerable<PaymentReadDto>>(payments));
            }

            [HttpGet("{paymentId}", Name = "GetPaymentForEnrollment")]
            public ActionResult<PaymentReadDto> GetPaymentForEnrollment(int enrollmentId, int paymentId)
            {
                Console.WriteLine($"--> GetPaymentForEnrollment: {enrollmentId} / {paymentId}");
                if (!_repository.EnrollmentExist(enrollmentId))
                {
                    return NotFound();
                }
                var payment = _repository.GetPayment(enrollmentId, paymentId);
                if (payment == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<PaymentReadDto>(payment));
            }

            [HttpPost]
            public ActionResult<PaymentReadDto> CreatePaymentForEnrollment(int enrollmentId, PaymentCreateDto paymentDto)
            {
                Console.WriteLine($"--> CreatePaymentForEnrollment: {enrollmentId}");
                if (!_repository.EnrollmentExist(enrollmentId))
                {
                    return NotFound();
                }

                var payment = _mapper.Map<Payment>(paymentDto);
                _repository.CreatePayment(enrollmentId, payment);
                _repository.SaveChanges();
                var paymentReadDto = _mapper.Map<PaymentReadDto>(payment);

                return CreatedAtRoute(nameof(GetPaymentForEnrollment),
                new { enrollmentId = enrollmentId, paymentId = paymentReadDto.Id }, paymentReadDto);
            }
        }
    }
}