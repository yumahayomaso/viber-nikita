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
}
