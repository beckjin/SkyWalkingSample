using Grpc.Core;
using Grpc.Core.Interceptors;
using GrpcService.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SkyApm;
using SkyApm.Agent.GeneralHost;
using SkyApm.Diagnostics.Grpc.Server;
using SkyApm.Diagnostics.MongoDB;
using SkyApm.Utilities.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace GrpcService1
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            StartGrpcServer(host.Services);
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return new HostBuilder()
                .AddSkyAPM()
                .ConfigureServices((hostContext, services) =>
                {
                    //services.AddSingleton<IUserRepository, GrpcService.Repositories.EF.UserRepository>();
                    services.AddSingleton<IUserRepository, GrpcService.Repositories.Mongodb.UserRepository>();

                    services.AddSkyApmExtensions().AddMongoDB();

                    services.AddSingleton<User.UserBase, UserImpl>();
                    services.AddAutoMapper(typeof(Program).Assembly);
                    services.AddDbContext<GrpcService.Repositories.EF.AppDbContext>(c => c.UseMySql("Server=localhost;Port=3306;Database=skywalking; User=root;Password=;"));
                });
        }

        public static IServiceProvider StartGrpcServer(IServiceProvider provider)
        {
            var interceptor = provider.GetService<ServerDiagnosticInterceptor>();
            var definition = User.BindService(provider.GetService<User.UserBase>());
            if (interceptor != null)
            {
                definition = definition.Intercept(interceptor);
            }
            int port = 5051;
            Server server = new Server
            {
                Services = { definition },
                Ports = { new ServerPort("0.0.0.0", port, ServerCredentials.Insecure) },
            };
            server.Start();

            Console.WriteLine("Listening on port " + port);
            return provider;
        }

    }
}
