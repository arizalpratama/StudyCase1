using PaymentService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PaymentService.Data
{
    public class PaymentRepo : IPaymentRepo
    {
        private readonly AppDbContext _context;

        public PaymentRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreateEnrollment(Enrollment enroll)
        {
            if (enroll == null)
                throw new ArgumentNullException(nameof(enroll));
            _context.Enrollments.Add(enroll);
        }

        public void CreatePayment(int enrollmentId, Payment payment)
        {
            if (payment == null)
                throw new ArgumentNullException(nameof(payment));
            payment.EnrollmentId = enrollmentId;
            _context.Payments.Add(payment);
        }

        public bool EnrollmentExist(int enrollmentid)
        {
            return _context.Enrollments.Any(p => p.Id == enrollmentid);
        }

        public bool ExternalEnrollmentExist(int externalEnrollmentId)
        {
            return _context.Enrollments.Any(p => p.ExternalID == externalEnrollmentId);
        }

        public IEnumerable<Enrollment> GetAllEnrollments()
        {
            return _context.Enrollments.ToList(); ;
        }

        public Payment GetPayment(int enrollmentId, int paymentId)
        {
            return _context.Payments
            .Where(c => c.EnrollmentId == enrollmentId && c.Id == paymentId)
            .FirstOrDefault();
        }

        public IEnumerable<Payment> GetPaymentsForEnrollment(int enrollmentid)
        {
            return _context.Payments
            .Where(c => c.EnrollmentId == enrollmentid)
            .OrderBy(c => c.Enrollment.Name);
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }
    }
}