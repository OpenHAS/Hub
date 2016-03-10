using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace HomeAutomation.Communication.Http
{
    public class WebServer
    {
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        public void StartListening(int port)
        {
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, port);

            try
            {
                listener.Bind(ipEndPoint);
                listener.Listen(5);

                while(true)
                {
                    allDone.Reset();

                    listener.AcceptAsync(new SocketAsyncEventArgs()
                    {
                        
                    });

                    allDone.WaitOne();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
