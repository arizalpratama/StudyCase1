using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EnrollmentService.Data;
using EnrollmentService.Dtos;
using EnrollmentService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnrollmentService.Interface;

namespace EnrollmentService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {

        private IUser _user;
        public UsersController(IUser user)
        {
            _user = user;
        }

        //Registration
        [HttpPost]
        public async Task<ActionResult> Registration(CreateUserDto user)
        {
            try
            {
                await _user.Registration(user);
                return Ok($"Registrasi user {user.Username} berhasil");
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        //Get All
        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetAll()
        {
            return Ok(_user.GetAllUser());
        }

        //Add Role
        [Authorize(Roles = "admin")]
        [HttpPost("Role/{roleName}")]
        public async Task<ActionResult> AddRole(string roleName)
        {
            try
            {
                await _user.AddRole(roleName);
                return Ok($"Tambah role {roleName} berhasil");
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        //Get Role
        Authorize(Roles = "admin")]
        [HttpGet("Role")]
        public ActionResult<IEnumerable<CreateRoleDto>> GetAllRole()
        {
            return Ok(_user.GetRoles());
        }

        //Add User To Role
        [Authorize(Roles = "admin")]
        [HttpPost("UserInRole")]
        public async Task<ActionResult> AddUserToRole(string username, string role)
        {
            try
            {
                await _user.AddUserToRole(username, role);
                return Ok($"Berhasil menambahkan {username} ke role {role}");
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        //Get Roles From User 
        [Authorize(Roles = "admin")]
        [HttpGet("RolesByUser/{username}")]
        public async Task<ActionResult<List<string>>> GetRolesByUser(string username)
        {
            var results = await _user.GetRolesFromUser(username);
            return Ok(results);
        }


        //Authentication
        [AllowAnonymous]
        [HttpPost("Authentication")]
        public async Task<ActionResult<User>> Authentication(CreateUserDto createUserDto)
        {
            try
            {
                var user = await _user.Authenticate(createUserDto.Username, createUserDto.Password);
                if (user == null)
                    return BadRequest("username/password tidak tepat");
                return Ok(user);
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
