using AutoMapper;
using PaymentService.Dtos;
using PaymentService.Models;

namespace PaymentService.Profiles
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<Enrollment, EnrollmentReadDto>();
            CreateMap<PaymentCreateDto, Payment>();
            CreateMap<Payment, PaymentReadDto>();
            CreateMap<EnrollmentPublishedDto, Enrollment>()
            .ForMember(dest => dest.ExternalID,
            opt => opt.MapFrom(src => src.Id));
        }
    }
}