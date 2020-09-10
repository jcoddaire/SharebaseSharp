using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using RestSharp.Serializers;

namespace Sharebase.API.Helpers {
    public class SharebaseSerializer : ISerializer {
        private readonly Newtonsoft.Json.JsonSerializer _serializer;

        /// <summary>
        /// Default serializer
        /// </summary>
        public SharebaseSerializer () {
            ContentType = "application/json";
            _serializer = new Newtonsoft.Json.JsonSerializer {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Include,
                DefaultValueHandling = DefaultValueHandling.Include,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
        }

        /// <summary>
        /// Default serializer with overload for allowing custom Json.NET settings
        /// </summary>
        public SharebaseSerializer (Newtonsoft.Json.JsonSerializer serializer) {
            ContentType = "application/json";
            serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            _serializer = serializer;
        }

        /// <summary>
        /// Serialize the object as JSON
        /// </summary>
        /// <param name="obj">Object to serialize</param>
        /// <returns>JSON as String</returns>
        public string Serialize (object obj) {
            using (var stringWriter = new StringWriter ()) {
                using (var jsonTextWriter = new JsonTextWriter (stringWriter)) {
                    jsonTextWriter.Formatting = Formatting.Indented;
                    jsonTextWriter.QuoteChar = '"';

                    _serializer.Serialize (jsonTextWriter, obj);

                    var result = stringWriter.ToString ();
                    return result;
                }
            }
        }

        /// <summary>
        /// Unused for JSON Serialization
        /// </summary>
        public string DateFormat { get; set; }
        /// <summary>
        /// Unused for JSON Serialization
        /// </summary>
        public string RootElement { get; set; }
        /// <summary>
        /// Unused for JSON Serialization
        /// </summary>
        public string Namespace { get; set; }
        /// <summary>
        /// Content type for serialized content
        /// </summary>
        public string ContentType { get; set; }
    }
}