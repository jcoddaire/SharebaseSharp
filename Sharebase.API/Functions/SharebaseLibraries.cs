using System.Collections.Generic;
using RestSharp;
using Sharebase.API.Models;
using Sharebase.API.Interfaces;

namespace Sharebase.API
{
    public partial class SharebaseSharp : ISharebaseSharp {
        
        public List<SharebaseLibrary> GetAllLibraries () {
            var request = new RestRequest ();
            request.Resource = @"/api/libraries";

            return Execute<List<SharebaseLibrary>> (request);
        }

        public SharebaseDirectory CreateDirectory (int libraryID, SharebaseDirectoryCreate parameters) {
            var request = new RestRequest ();
            request.Method = Method.POST;
            request.Resource = @"api/libraries/{libraryId}/folders";
            request.AddUrlSegment ("libraryId", libraryID);
            request.AddJsonBody (parameters);

            return Execute<SharebaseDirectory> (request);
        }
    }
}