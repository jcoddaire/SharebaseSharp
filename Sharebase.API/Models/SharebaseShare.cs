using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;


namespace Sharebase.API.Models
{
    public class SharebaseShare
    {
        [JsonProperty (PropertyName = "ExpireStyle")]
        public SharebaseExpireStyle ExpireStyle { get; set; } = SharebaseExpireStyle.never;
        
        [JsonProperty (PropertyName = "ExpiresOn")]
        public DateTime? ExpiresOn { get; set; }
        
        [JsonProperty (PropertyName = "ExpirationValue")]
        public int ExpirationValue { get; set; } = 0;
        
        [JsonProperty (PropertyName = "PinRequired")]
        public bool PinRequired { get; set; } = false;
        
        [JsonProperty (PropertyName = "AccessCodeRequired")]
        public bool AccessCodeRequired { get; set; } = false;
        
        [JsonProperty (PropertyName = "Recipients")]
        public string[] Recipients { get; set; } = null;
        
        [JsonProperty (PropertyName = "Password")]
        public string Password { get; set; } = null;
        
        [JsonProperty (PropertyName = "AllowView")]
        public bool AllowView { get; set; } = true;
        
        [JsonProperty (PropertyName = "AllowDownload")]
        public bool AllowDownload { get; set; } = false;
        
        [JsonProperty (PropertyName = "AllowUpload")]
        public bool AllowUpload { get; set; } = false;
        
        [JsonProperty (PropertyName = "NotifyRecipients")]
        public bool NotifyRecipients { get; set; } = true;
        
        [JsonProperty (PropertyName = "CustomEmailText")]
        public string CustomEmailText { get; set; } = null;
    }
}