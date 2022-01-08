using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EnrollmentService.Data;
using EnrollmentService.Dtos;
using EnrollmentService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnrollmentService.Interface;

namespace EnrollmentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {

        private IEnrollment _enrollment;
        private IMapper _mapper;

        public EnrollmentsController(IEnrollment enrollment, IMapper mapper)
        {
            _enrollment = enrollment ?? throw new ArgumentNullException(nameof(enrollment));
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
        }


        //GetAll
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnrollmentDto>>> Get()

        {
            var enrollments = await _enrollment.GetAll();
            var dtos = _mapper.Map<IEnumerable<EnrollmentDto>>(enrollments);
            return Ok(dtos);

        }

        //Get By Id
        [HttpGet("{id}")]
        public async Task<ActionResult<EnrollmentDto>> Get(int id)
        {
            var result = await _enrollment.GetById(id.ToString());
            if (result == null)
                return NotFound();


            return Ok(_mapper.Map<EnrollmentDto>(result));
        }

        //Insert
        [HttpPost]
        public async Task<ActionResult<EnrollmentDto>> Post([FromBody] EnrollmentForCreateDto enrollmentforCreateDto)
        {
            try
            {
                var enrollment = _mapper.Map<Enrollment>(enrollmentforCreateDto);
                var result = await _enrollment.Insert(enrollment);
                var enrollmentReturn = _mapper.Map<EnrollmentDto>(result);
                return Ok(enrollmentReturn);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Update
        [HttpPut("{id}")]
        public async Task<ActionResult<EnrollmentDto>> Put(int id, [FromBody] EnrollmentForCreateDto enrollmentForCreateDto)
        {
            try
            {
                var enrollment = _mapper.Map<Enrollment>(enrollmentForCreateDto);
                var result = await _enrollment.Update(id.ToString(), enrollment);
                var enrollmentdto = _mapper.Map<EnrollmentDto>(result);
                return Ok(enrollmentdto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        //Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _enrollment.Delete(id.ToString());
                return Ok($"Data course {id} berhasil di delete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}