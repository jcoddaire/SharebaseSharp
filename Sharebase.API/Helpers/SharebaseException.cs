using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;

namespace Sharebase.API.Helpers {
    public class SharebaseException : Exception {
        private string message;

        public IRestResponse Response;

        public SharebaseException (IRestResponse response) {
            Response = response;
            message = "Unexpected response status " + ((int) response.StatusCode).ToString () + " with body:\n" + response.Content;
        }

        public override string Message {
            get { return message; }
        }
    }
}