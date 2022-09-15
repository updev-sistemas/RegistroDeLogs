namespace RegistroDeLogs
{
    public class Worker : BackgroundService
    {
        private readonly IHostApplicationLifetime? _appLifetime;
        private readonly string path = @"C:\Logs\saida.txt";
        public Worker(IHostApplicationLifetime appLifetime)
        {
            this._appLifetime = appLifetime;

            this._appLifetime.ApplicationStarted.Register(OnStarted);
            this._appLifetime.ApplicationStopping.Register(OnStopping);
            this._appLifetime.ApplicationStopped.Register(OnStopped);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                File.AppendAllText(path, $"Worker running at: {DateTimeOffset.Now} {Environment.NewLine}");
                await Task.Delay(1000, stoppingToken);
            }
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            File.AppendAllText(path, $"1. StartAsync has been called. {Environment.NewLine}");

            await base.StartAsync(cancellationToken).ConfigureAwait(false);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            File.AppendAllText(path, $"4. StopAsync has been called.{Environment.NewLine}");

            await base.StopAsync(cancellationToken).ConfigureAwait(false);
        }

        private void OnStarted()
        {
            File.AppendAllText(path, $"2. OnStarted has been called.{Environment.NewLine}");
        }

        private void OnStopping()
        {
            File.AppendAllText(path, $"3. OnStopping has been called.{Environment.NewLine}");
        }

        private void OnStopped()
        {
            File.AppendAllText(path, $"5. OnStopped has been called.{Environment.NewLine}");
        }
    }
}