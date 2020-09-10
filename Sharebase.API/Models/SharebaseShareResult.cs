using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Sharebase.API.Models
{
    public class SharebaseShareResult
    {
        [JsonProperty (PropertyName = "ShareId")]
        public int ShareId { get; set; }
        
        [JsonProperty (PropertyName = "FolderId")]
        public int FolderId { get; set; }
        
        [JsonProperty (PropertyName = "ReferenceString")]
        public string ReferenceString { get; set; }
        
        [JsonProperty (PropertyName = "Status")]
        public string Status { get; set; }
        
        [JsonProperty (PropertyName = "Link")]
        public string Link { get; set; }
        
        [JsonProperty (PropertyName = "Links")]
        public Dictionary<string, string> Links { get; set; }
    }
}