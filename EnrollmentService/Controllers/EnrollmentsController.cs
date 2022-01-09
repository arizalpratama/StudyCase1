﻿using AutoMapper;
using EnrollmentService.Dtos;
using EnrollmentService.Interface;
using EnrollmentService.SyncDataServices.Http;
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
        [HttpGet]
        public ActionResult<IEnumerable<EnrollmentDto>> GetEnrollments()
        {
            Console.WriteLine("--> Getting Enrollments .....");
            var enrollmentItem = _repository.GetAllEnrollments();
            return Ok(_mapper.Map<IEnumerable<EnrollmentDto>>(enrollmentItem));
        }

        //Get By Id
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


    }
}