using EnrollmentService.Dtos;
using System.Threading.Tasks;

namespace EnrollmentService.SyncDataServices.Http
{
    public interface IPaymentDataClient
    {
        Task SendEnrollmentToPayment(EnrollmentDto enroll);
    }
}
