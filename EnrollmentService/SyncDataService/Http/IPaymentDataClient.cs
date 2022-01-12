using EnrollmentService.Dtos;
using EnrollmentService.Models;
using System.Threading.Tasks;

namespace EnrollmentService.SyncDataService.Http
{
    public interface IPaymentDataClient
    {
        Task SendEnrollmentToPayment(EnrollmentReadDto enroll);
    }
}