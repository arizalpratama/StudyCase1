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
    public class StudentsController : ControllerBase
    {

        private IStudent _student;
        private IMapper _mapper;

        public StudentsController(IStudent student, IMapper mapper)
        {
            _student = student ?? throw new ArgumentNullException(nameof(student));
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
        }

        //Get All
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> Get()
        {
            var students = await _student.GetAll();
            var dtos = _mapper.Map<IEnumerable<StudentDto>>(students);
            return Ok(dtos);
        }

        //Get By Id
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> Get(int id)
        {
            var result = await _student.GetById(id.ToString());
            if (result == null)
                return NotFound();

            return Ok(_mapper.Map<StudentDto>(result));
        }

        //Insert
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<StudentDto>> Post([FromBody] StudentForCreateDto studentforCreateDto)
        {
            try
            {
                var student = _mapper.Map<Student>(studentforCreateDto);
                var result = await _student.Insert(student);
                var studentReturn = _mapper.Map<StudentDto>(result);
                return Ok(studentReturn);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Update
        [HttpPut("{id}")]
        public async Task<ActionResult<StudentDto>> Put(int id, [FromBody] StudentForCreateDto studentForCreateDto)
        {
            try
            {
                var student = _mapper.Map<Student>(studentForCreateDto);
                var result = await _student.Update(id.ToString(), student);
                var studentdto = _mapper.Map<StudentDto>(result);
                return Ok(studentdto);
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
                await _student.Delete(id.ToString());
                return Ok($"Data student {id} berhasil di delete");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
