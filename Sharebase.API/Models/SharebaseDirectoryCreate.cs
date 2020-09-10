using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Sharebase.API.Models {
    public class SharebaseDirectoryCreate {
        [JsonProperty (PropertyName = "FolderPath")]
        public string FolderPath { get; set; }
    }
}