using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EnrollmentService.Dtos;
using EnrollmentService.Models;
using Microsoft.Extensions.Configuration;

namespace EnrollmentService.SyncDataService.Http
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

        public async Task SendEnrollmentToPayment(EnrollmentReadDto enroll)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(enroll),
                Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_configuration["PaymentService"],
                httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to PaymentService Was OK !");
            }
            else
            {
                Console.WriteLine("--> Sync POST to PaymentService Failed");
            }
        }
    }
}