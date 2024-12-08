using Core.Models;
using Core.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Users
{
    public class IndexModel : PageModel {
        private readonly HttpClient _httpClient;

        public IndexModel(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public List<UserDto> Users { get; set; } = new();
        public string Message { get; set; } = "";
        public bool Status { get; set; }

        public async Task OnGetAsync() {
            try {
                var currentHost = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
                var apiUrl = new Uri(new Uri(currentHost), "api/Users");

                var httpResponseMessage = await _httpClient.GetAsync(apiUrl);
                if (httpResponseMessage.IsSuccessStatusCode) {
                    if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.NoContent)
                        Message = "Нет данных";
                    else {
                        Users = await httpResponseMessage.Content.ReadFromJsonAsync<List<UserDto>>();
                        if (Users == null) {
                            Message = "Непредвиденная ошибка.";
                        }
                    }
                }
                else {
                    Message = await httpResponseMessage.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex) {
                Message = "Непредвиденная ошибка.";
            }
        }

        public async Task OnPostAsync() {

        }
    }
}
