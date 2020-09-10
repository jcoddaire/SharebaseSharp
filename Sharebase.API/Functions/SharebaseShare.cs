using System.Collections.Generic;
using RestSharp;
using Sharebase.API.Models;
using Sharebase.API.Interfaces;
using System;

namespace Sharebase.API
{
    public partial class SharebaseSharp : ISharebaseSharp {
        
        public SharebaseShareResult CreateShare(int folderId, SharebaseShare shareRequirements){

            if(folderId <= 0){
                throw new ArgumentOutOfRangeException("folderId");
            }
            if(shareRequirements == null){
                throw new ArgumentNullException("shareRequirements");
            }

            var request = new RestRequest ();
            request.Method = Method.POST;
            request.Resource = @"api/folders/{folderId}/share";
            request.AddUrlSegment ("folderId", folderId);
            request.AddJsonBody (shareRequirements);

            return Execute<SharebaseShareResult> (request);
        }
    }
}