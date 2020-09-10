using System.Collections.Generic;
using Sharebase.API.Models;

namespace Sharebase.API.Interfaces
{
    public interface ISharebaseSharp
    {
         List<SharebaseLibrary> GetAllLibraries ();
         SharebaseDirectory CreateDirectory (int libraryID, SharebaseDirectoryCreate parameters);
         SharebaseFile UploadDocument (int folderId, string filePath, string fileName);
         SharebaseShareResult CreateShare(int folderId, SharebaseShare shareRequirements);
    }
}