using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Sharebase.API.Models
{
    public class UploadLinks
    {
        [JsonProperty (PropertyName = "Location")]
        public string Location { get; set; }
    }
}