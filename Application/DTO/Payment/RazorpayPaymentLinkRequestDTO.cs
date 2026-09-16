using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Application.DTO.Payment
{
    public class RazorpayPaymentLinkRequestDTO
    {
        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("reference_id")]
        public string ReferenceId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("customer")]
        public CustomerDTO Customer { get; set; }

        [JsonProperty("reminder_enable")]
        public bool ReminderEnable { get; set; }

        [JsonProperty("callback_url")]
        public string CallbackUrl { get; set; }

        [JsonProperty("callback_method")]
        public string CallbackMethod { get; set; }
    }

    public class CustomerDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("contact")]
        public string Contact { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }

}
