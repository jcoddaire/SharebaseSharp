using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sharebase.API.Models;
using Sharebase.API.Interfaces;
using Sharebase.API.Helpers;

namespace Sharebase.API
{
    public partial class SharebaseSharp : ISharebaseSharp {
        const long SMALL_LARGE_FILE_THRESHOLD = 5000000; // 5MB
        private int chunkSize = 1024 * 512; // This is the current chunk size in SB.
        
        public SharebaseFile UploadDocument (int folderId, string filePath, string fileName) {

            // TODO: logic for if a file already exists - do update instead.

            // get the file size of document.
            long currentFileSize = new System.IO.FileInfo (filePath).Length;

            if (currentFileSize >= SMALL_LARGE_FILE_THRESHOLD) {
                // big upload
                var result = LargeUpload(folderId, filePath, fileName);
                return result;
            } else {
                // small upload.
                var result = SmallUpload (folderId, filePath, fileName);                
                // TODO: do something with result.                
            }

            return null;
        }

        private async Task<HttpResponseMessage> SmallUpload (int folderID, string filePath, string fileName) {
            // This code was fetched from the API documentation.
            // It's not clear how to get this to work with the RestSharp library, but if you know how to do that
            // shoot an example my way.

            var docNameAsJSON = $"{{\"DocumentName\":\"{fileName}\"}}";

            HttpClient client = new HttpClient ();
            client.DefaultRequestHeaders.Add ("Authorization", _bearerToken);
            var uri = $"{_BaseURL}/api/folders/{folderID}/documents";

            using (var fs = new FileStream (filePath, FileMode.Open, FileAccess.Read)) {
                using (var multipartcontent = new MultipartFormDataContent ()) {
                    using (var filecontent = new StreamContent (fs)) {
                        using (var metadata = new StringContent (docNameAsJSON)) {
                            multipartcontent.Add (filecontent, "file", fileName);
                            multipartcontent.Add (metadata, "metadata");
                            return await client.PostAsync(uri, multipartcontent);                            
                            // TODO: update this to return an object, not a web response.
                            // It works for now and I don't plan on doing anything with it for now,
                            // so this waits for another day.
                        }
                    }
                }
            }
        }

        private SharebaseFile LargeUpload(int folderID, string filePath, string fileName)
        {
            var requestManager = new RequestManager(_MachineName, _bearerToken);

            //Initialize large document upload.
            var uploadObject = InitializeLargeUpload(requestManager, folderID, fileName);

            //Large document upload in chunks.
            uploadObject = LargeDocumentUpload(requestManager, uploadObject, filePath);

            //Finalize large document upload.
            return FinalizeLargeUpload(requestManager, uploadObject, folderID);
        }

        /*
            Jacob Coddaire
            September 3, 2020

            PLEASE NOTE: all "large file upload" methods have been copied directly from Sharebase's API.
            https://developers.sharebase.com/upload-large-document-example/

            They have been slightly modified to match the current library structure.
            They do not use the RestSharp library.
            In the future I may refactor them to use RestSharp but as it stands I do not have time to do so now
            and the current functionality works.

        */
        #region LargeDocumentUploadMethods
        private UploadObject InitializeLargeUpload(RequestManager manager, int folderId, string fileName)
        {
            HttpWebRequest request = manager.GeneratePOSTRequest($"{_BaseURL}/api/folders/{folderId}/temp?filename={fileName}", string.Empty, string.Empty, string.Empty, true);
            HttpWebResponse response = manager.GetResponse(request);
            var content = manager.GetResponseContent(response);
            return JsonConvert.DeserializeObject<UploadObject>(content);
        }

        private UploadObject LargeDocumentUpload(RequestManager manager, UploadObject uploadObject, string filePath)
        {
            var largeFileSize = new System.IO.FileInfo(filePath).Length;
            var toBeReadFileSize = largeFileSize;

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            byte[] toBeUploadedContent = GetNextNChucks(ref fileStream, ref toBeReadFileSize);

            uploadObject = UploadInChunks(manager, uploadObject.Links.Location, toBeUploadedContent);
            while (uploadObject.CurrentSize < largeFileSize)
            {
                toBeUploadedContent = GetNextNChucks(ref fileStream, ref toBeReadFileSize);
                uploadObject = UploadInChunks(manager, uploadObject.Links.Location, toBeUploadedContent);
            }

            fileStream.Close();
            return uploadObject;
        }

        private SharebaseFile FinalizeLargeUpload(RequestManager manager, UploadObject obj, int folderID)
        {
            HttpWebRequest request = manager.GeneratePOSTRequest($"{_BaseURL}/api/folders/{folderID}/documents", string.Empty, string.Empty, string.Empty, true);
            request.Headers.Add("x-sharebase-fileref", JsonConvert.SerializeObject(obj));
            HttpWebResponse response = manager.GetResponse(request);
            if(response == null){
                // the file may already exist. Return nothing important.
                return null;
            }
            var content = manager.GetResponseContent(response);
            return JsonConvert.DeserializeObject<SharebaseFile>(content);

        }
        private UploadObject UploadInChunks(RequestManager manager, string uri, byte[] contentChunk)
        {
            HttpWebRequest request = manager.GenerateRequest(uri, contentChunk, "PATCH", string.Empty, string.Empty, true);
            HttpWebResponse response = manager.GetResponse(request);
            var content = manager.GetResponseContent(response);
            return JsonConvert.DeserializeObject<UploadObject>(content);
        }
        private byte[] GetNextNChucks(ref FileStream fileStream, ref long toBeReadFileSize)
        {
            if (toBeReadFileSize < chunkSize)
            {
                chunkSize = (int)toBeReadFileSize;
            }
            byte[] buffer = new byte[chunkSize];
            fileStream.Read(buffer, 0, buffer.Length);
            toBeReadFileSize = toBeReadFileSize - chunkSize;
            return buffer;
        }

        #endregion
    }
}