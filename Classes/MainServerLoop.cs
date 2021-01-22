using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SphubCore.Classes.Server;
using System.Net;
using System.Net.Sockets;
using SphubCore.Reporting;
using SphubCore.Classes;
using SphubInterceptServer;
using SphubCore.Classes.Toolkit;

namespace SphubInterceptServer
{
    public class MainServerLoop
    {
        private bool Runserver = true;

        public void Run()
        {
            try
            {
                Console.WriteLine("Server Started on:");
                Console.WriteLine(SphubCore.Classes.Console.Config.CL_LOC_IP());
                Console.WriteLine("Begining Main Loop");

                while (Runserver)
                {
                    Socket s = Runtime.StartServer();
                    byte[] b = new byte[100];
                    int k = s.Receive(b);
                    Console.WriteLine("Command Received...");
                    string parsedCommand = "";

                    for (int i = 0; i < k; i++)
                    {
                        parsedCommand = parsedCommand + Convert.ToChar(b[i]);
                    }
                    Console.WriteLine(Base64.Decode(parsedCommand));
                    var ResponseData = ResponseHandler.Received(parsedCommand);
                    ASCIIEncoding asen = new ASCIIEncoding();
                    //what to do with data
                    if (Int32.Parse(ResponseData[0]) > 0)
                    {
                        //Begin Storage Client
                    }
                    //response to client
                    s.Send(asen.GetBytes(ResponseHandler.ResponseSendBack(Int32.Parse(ResponseData[0]))));
                    Console.WriteLine("Reply Sent");
                    s.Close();
                }
                Runtime.StopServer();
            }
            catch (Exception e)
            {
                Reporting.ER(e);
            }
        }
    }
}