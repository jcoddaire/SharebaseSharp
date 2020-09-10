using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Sharebase.API.Models
{
    public class UploadObject
    {
        [JsonProperty (PropertyName = "Links")]
        public UploadLinks Links { get; set; }
        
        [JsonProperty (PropertyName = "Identifier")]
        public string Identifier { get; set; }
        
        [JsonProperty (PropertyName = "FileName")]
        public string FileName { get; set; }
        
        [JsonProperty (PropertyName = "CurrentSize")]
        public long CurrentSize { get; set; }
        
        [JsonProperty (PropertyName = "VolumeId")]
        public long VolumeId { get; set; }
    }
}