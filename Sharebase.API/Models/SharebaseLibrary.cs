using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Sharebase.API.Models {
    public class SharebaseLibrary {
        [JsonProperty (PropertyName = "LibraryId")]
        public int LibraryId { get; set; }

        [JsonProperty (PropertyName = "LibraryName")]
        public string LibraryName { get; set; }

        [JsonProperty (PropertyName = "IsPrivate")]
        public bool IsPrivate { get; set; }

        [JsonProperty (PropertyName = "Links")]
        public Dictionary<string, string> Links { get; set; }
    }
}