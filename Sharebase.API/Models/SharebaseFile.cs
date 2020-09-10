using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Sharebase.API.Models {
    public class SharebaseFile {

        [JsonProperty (PropertyName = "DocumentId")]
        public float DocumentId { get; set; }

        [JsonProperty (PropertyName = "DocumentName")]
        public string DocumentName { get; set; }

        [JsonProperty (PropertyName = "FolderId")]
        public float FolderId { get; set; }

        [JsonProperty (PropertyName = "DateModified")]
        public DateTime? DateModified { get; set; }

        [JsonProperty (PropertyName = "Hash")]
        public string Hash { get; set; }

        [JsonProperty (PropertyName = "Links")]
        public Dictionary<string, string> Links { get; set; }
    }
}