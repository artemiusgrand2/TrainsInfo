using System;
using System.IO;
using System.Net;
using System.Security.Permissions;
using System.Security.Principal;
using System.Runtime.InteropServices;

namespace Ex
{
    class Program
    {
        //[DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        //public static extern bool LogonUser(String lpszUsername, String lpszDomain, String lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

        //static void Main(string[] args)
        //{
        //    //WebClient request = new WebClient();

        //    //request.Credentials = new NetworkCredential("10.200.2.110\\ktc", "Ctk1234");
        //    //var f = args[0].ToArray();
        //   // Directory.GetDirectories(@"\\10.200.2.110\ktc");

      //  var req = FileWebRequest.Create(new Uri(@"\\10.200.2.110\ktc\MejGosStiki.csv"));
        
        //req.Credentials = new NetworkCredential(@"<Domain>\<User>", "<Password>");
        //req.PreAuthenticate = true;

        //WebResponse d = req.GetResponse();
        //FileStream fs = File.Create("test.txt");

        //// here you can check that the cast was successful if you want. 
        //fs = d.GetResponseStream() as FileStream;
        //fs.Close();

        //    NetworkCredential theNetworkCredential = new NetworkCredential("10.200.2.110\\ktc", "Ctk1234");
        //    CredentialCache theNetCache = new CredentialCache();
        //    theNetCache.Add(new Uri(@"\\10.200.2.110"), "Basic", theNetworkCredential);
        //    string[] theFolders = Directory.GetDirectories(@"\\10.200.2.110\ktc");

        //    //if (File.Exists(local))
        //    //{
        //    //    File.Delete(local);

        //    //    File.Copy(remote, local, true);
        //    //}
        //}
    }
}
