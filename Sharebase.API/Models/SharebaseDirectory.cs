using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Sharebase.API.Models {
    public class SharebaseDirectory {

        [JsonProperty (PropertyName = "FolderId")]
        public int FolderId { get; set; }

        [JsonProperty (PropertyName = "FolderName")]
        public string FolderName { get; set; }

        [JsonProperty (PropertyName = "LibraryId")]
        public int LibraryId { get; set; }

        [JsonProperty (PropertyName = "Links")]
        public Dictionary<string, string> Links { get; set; }

        [JsonProperty (PropertyName = "Embedded")]
        public Dictionary<string, object> Embedded { get; set; }
    }
}