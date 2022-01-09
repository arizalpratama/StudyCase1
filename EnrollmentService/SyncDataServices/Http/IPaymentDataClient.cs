using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnrollmentService.Dtos;

namespace EnrollmentService.SyncDataServices.Http
{
    public interface IPaymentDataClient
    {
        Task SendEnrollmentToPayment(EnrollmentDto enrol);
    }
}