using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using EnrollmentService.Dtos;

namespace EnrollmentService.SyncDataServices.Http
{
    public class HttpPaymentDataClient : IPaymentDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpPaymentDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task SendEnrollmentToPayment(EnrollmentDto enroll)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(enroll),
                Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_configuration["PaymentService"],
                httpContent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to PaymentService was OK !");
            }
            else
            {
                Console.WriteLine("--> Sync POST to PaymentService failed");
            }
        }
    }
}