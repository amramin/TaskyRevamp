using TaskyRevamp.Dto;
using System.Net.Http.Json;
using TaskyRevamp.Dto.Email;
namespace TaskyRevamp.Client.Consumer
{
    public class SendEmailConsumer
    {
        private readonly TaskyService _TaskyService;

        public SendEmailConsumer(TaskyService TaskyService)
        {
            _TaskyService = TaskyService;
        }


        public async Task SendEmail(SendEmailDto SendEmail)
        {
            await _TaskyService.PostAsJsonAsync($"api/SendEmail/", SendEmail);
        }







    }
}
