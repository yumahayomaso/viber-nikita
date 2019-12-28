using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ViberApi.Controllers
{

    public class SetWebhookRequest
    {
        public string url { get; set; }
        public List<string> event_types { get; set; }
        public bool send_name { get; set; }
        public bool send_photo { get; set; }
    }

    [Route("viber")]
    public class ViberController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        public ViberController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("viber");
            _httpClient.DefaultRequestHeaders.Add("X-Viber-Auth-Token", "4ad2fe7608a7ded5-590bfc5f40a8645e-a4e55995ffbada7b");
        }

        [HttpPost("setwebhook")]
        public async Task<IActionResult> SetWebhook([FromBody] SetWebhookRequest request)
        {
            var response = await _httpClient.PostAsync("https://chatapi.viber.com/pa/set_webhook",
                new StringContent(JsonConvert.SerializeObject(request)));
            return Ok(await response.Content.ReadAsStringAsync());
        }

        [HttpPost("message")]
        public async Task<IActionResult> Send([FromBody] SendMessageRequest request)
        {
            var response = await _httpClient.PostAsync("https://chatapi.viber.com/pa/send_message",
                new StringContent(JsonConvert.SerializeObject(request)));

            return Ok(await response.Content.ReadAsStringAsync());
        }

        [HttpPost("getuser")]
        public async Task<IActionResult> GetUser([FromBody] GetUserRequest request)
        {
            var response = await _httpClient.PostAsync("https://chatapi.viber.com/pa/get_user_details",
                new StringContent(JsonConvert.SerializeObject(request)));

            return Ok(await response.Content.ReadAsStringAsync());
        }


        [HttpPost("hook")]
        public async Task<IActionResult> Hook()
        {
            return Ok();
        }
    }

    public class GetUserRequest
    {
        public string Id { get; set; }
    }
    public class SendMessageRequest
    {
        [JsonProperty("receiver")]
        public string Receiver { get; set; }

        [JsonProperty("min_api_version")]
        public long MinApiVersion { get; set; }

        [JsonProperty("sender")]
        public Sender Sender { get; set; }

        [JsonProperty("tracking_data")]
        public string TrackingData { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class Sender
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("avatar")]
        public Uri Avatar { get; set; }
    }

    public partial class UserInfo
    {
        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("status_message")]
        public string StatusMessage { get; set; }

        [JsonProperty("message_token")]
        public double MessageToken { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }

    public partial class User
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("avatar")]
        public Uri Avatar { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("primary_device_os")]
        public string PrimaryDeviceOs { get; set; }

        [JsonProperty("api_version")]
        public long ApiVersion { get; set; }

        [JsonProperty("viber_version")]
        public string ViberVersion { get; set; }

        [JsonProperty("mcc")]
        public long Mcc { get; set; }

        [JsonProperty("mnc")]
        public long Mnc { get; set; }

        [JsonProperty("device_type")]
        public string DeviceType { get; set; }
    }
}
}
