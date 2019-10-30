using System;
using System.Net;
using System.IO;

namespace Ex
{
    class Program
    {

        //[DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        //public static extern bool LogonUser(String lpszUsername, String lpszDomain, String lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

        static void Main(string[] args)
        {
            WebRequest request = WebRequest.Create("http://localhost/AGDPNew/Service/CurrentPositionTrains?categoryTrain=3");

            WebResponse response = request.GetResponse();

            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            Console.ReadLine();
            ////WebClient request = new WebClient();
            //var f = new List<string>();
            //var f1 = f;
            //f1.Add("1");
            //f1.Clear();
            ////request.Credentials = new NetworkCredential("10.200.2.110\\ktc", "Ctk1234");
            ////var f = args[0].ToArray();
            //// Directory.GetDirectories(@"\\10.200.2.110\ktc");
            Console.WriteLine(DateTime.Now.ToShortDateString());
            //var req = FileWebRequest.Create(new Uri(@"\\10.200.2.110\ktc\MejGosStiki.csv"));

            //req.Credentials = new NetworkCredential(@"<Domain>\<User>", "<Password>");
            //req.PreAuthenticate = true;

            //WebResponse d = req.GetResponse();
            //FileStream fs = File.Create("test.txt");

            //// here you can check that the cast was successful if you want. 
            //fs = d.GetResponseStream() as FileStream;
            //fs.Close();

            //NetworkCredential theNetworkCredential = new NetworkCredential("10.200.2.110\\ktc", "Ctk1234");
            //CredentialCache theNetCache = new CredentialCache();
            //theNetCache.Add(new Uri(@"\\10.200.2.110"), "Basic", theNetworkCredential);
            //string[] theFolders = Directory.GetDirectories(@"\\10.200.2.110\ktc");

            ////if (File.Exists(local))
            ////{
            ////    File.Delete(local);

            ////    File.Copy(remote, local, true);
            ////}

            //HttpListener listener = new HttpListener();
            //// установка адресов прослушки
            //listener.Prefixes.Add("http://*:8888/");
            //listener.Prefixes.Add("http://localhost:8888/");
            //listener.Start();
            //Console.WriteLine("Ожидание подключений...");
            //// метод GetContext блокирует текущий поток, ожидая получение запроса 
            //HttpListenerContext context = listener.GetContext();

            //HttpListenerRequest request = context.Request;
           
            //// получаем объект ответа
            //HttpListenerResponse response = context.Response;
            //// создаем ответ в виде кода html
            //string responseStr = "<html><head><meta charset='utf8'></head><body>Привет мир!</body></html>";
            //byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseStr);
            //// получаем поток ответа и пишем в него ответ
            //response.ContentLength64 = buffer.Length;
            //Stream output = response.OutputStream;
            //output.Write(buffer, 0, buffer.Length);
            //// закрываем поток
            //output.Close();
            //// останавливаем прослушивание подключений
            //listener.Stop();
            //Console.WriteLine("Обработка подключений завершена");
            //Console.Read();
        }
    }

}
