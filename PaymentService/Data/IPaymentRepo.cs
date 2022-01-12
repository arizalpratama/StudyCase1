using PaymentService.Models;
using System.Collections.Generic;

namespace PaymentService.Data
{
    public interface IPaymentRepo
    {
        bool SaveChanges();
        IEnumerable<Enrollment> GetAllEnrollments();
        void CreateEnrollment(Enrollment enroll);
        bool EnrollmentExist(int enrollmentid);
        bool ExternalEnrollmentExist(int externalEnrollmentId);

        IEnumerable<Payment> GetPaymentsForEnrollment(int enrollmentid);
        Payment GetPayment(int enrollmentId, int paymentId);
        void CreatePayment(int enrollmentId, Payment payment);
    }
}