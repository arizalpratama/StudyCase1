using AutoMapper;
using EnrollmentService.Dtos;
using EnrollmentService.Models;

namespace EnrollmentService.Profiles
{
    public class EnrollmentsProfile : Profile
    {
        public EnrollmentsProfile()
        {
           /* CreateMap<Enrollment, EnrollmentDto>();
            CreateMap<Enrollment, EnrollmentForCreateDto>();*/

            CreateMap<Enrollment, EnrollmentReadDto>();
            CreateMap<EnrollmentCreateDto, Enrollment>();
            CreateMap<EnrollmentReadDto, EnrollmentPublishedDto>();
        }
    }
}
