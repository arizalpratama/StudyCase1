using EnrollmentService.Dtos;
using EnrollmentService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnrollmentService.Interface
{
    public interface IUser
    {
        //Get All
        IEnumerable<UserDto> GetAllUser();

        //Registration
        Task Registration(CreateUserDto user);

        //Add Roles
        Task AddRole(string rolename);

        //Get Roles
        IEnumerable<CreateRoleDto> GetRoles();

        //Add User To Roles
        Task AddUserToRole(string username, string role);

        //Get Roles From User
        Task<List<string>> GetRolesFromUser(string username);

        //Authentication
        Task<User> Authenticate(string username, string password);
    }
}
