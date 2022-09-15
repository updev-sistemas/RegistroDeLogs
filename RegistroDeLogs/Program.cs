using RegistroDeLogs;

IHost host = Host.CreateDefaultBuilder(args)
#if DEBUG
                .UseConsoleLifetime()
#else
                .UseWindowsService()
#endif
                .ConfigureServices(services =>
                {
                    services.AddHostedService<Worker>();
                })
                .Build();

await host.RunAsync();
