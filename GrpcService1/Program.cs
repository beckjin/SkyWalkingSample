using Grpc.Core;
using System;

namespace GrpcService1
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = User.BindService(new UserImpl());
            Server server = new Server
            {
                Services = { service },
                Ports = { new ServerPort("0.0.0.0", 5051, ServerCredentials.Insecure) }
            };
            server.Start();

            Console.WriteLine($"Listening on port {5051}");
            server.ShutdownTask.Wait();
        }
    }
}
