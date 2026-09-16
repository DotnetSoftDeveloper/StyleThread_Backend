using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RazorpayPaymentLinkResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("short_url")]
        public string ShortUrl { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("accept_partial")]
        public bool AcceptPartial { get; set; }

        [JsonProperty("first_min_partial_amount")]
        public int FirstMinPartialAmount { get; set; }

        [JsonProperty("expire_by")]
        public int ExpireBy { get; set; }

        [JsonProperty("reference_id")]
        public string ReferenceId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("customer")]
        public CustomerResponse Customer { get; set; }

        [JsonProperty("notify")]
        public NotifyResponse Notify { get; set; }

        [JsonProperty("reminder_enable")]
        public bool ReminderEnable { get; set; }

        [JsonProperty("notes")]
        public NotesResponse Notes { get; set; }

        [JsonProperty("callback_url")]
        public string CallbackUrl { get; set; }

        [JsonProperty("callback_method")]
        public string CallbackMethod { get; set; }

        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public long UpdatedAt { get; set; }
    }

    public class CustomerResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("contact")]
        public string Contact { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }

    public class NotifyResponse
    {
        [JsonProperty("sms")]
        public bool Sms { get; set; }

        [JsonProperty("email")]
        public bool Email { get; set; }
    }

    public class NotesResponse
    {
        [JsonProperty("policy_name")]
        public string PolicyName { get; set; }
    }
}
