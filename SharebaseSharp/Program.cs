using System;
using Sharebase.API.Interfaces;

namespace SharebaseSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello yes this is a demo.");

            // probably ought to get these from whatever configs / libraries you use.
            var machineName = System.Environment.MachineName;
            var sharebaseToken = "Bearer 123567abcdefghijk";
            const string sharebaseBaseURL = @"https://app.sharebase.com/sharebaseapi";

            // Create the sharebase service.
            ISharebaseSharp sharebaseService = new Sharebase.API.SharebaseSharp(sharebaseToken, sharebaseBaseURL, machineName);

            // Call the function, do something with the result.
            var allLibrariesTheAccountHasAccessTo = sharebaseService.GetAllLibraries();
        }
    }
}
