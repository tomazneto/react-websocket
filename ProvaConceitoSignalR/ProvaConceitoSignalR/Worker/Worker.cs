using Confluent.Kafka;
using Microsoft.AspNetCore.SignalR;
using ProvaConceitoSignalR.Hub;
using ProvaConceitoSignalR.Model;
using System.Text.Json;

namespace ProvaConceitoSignalR.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IHubContext<ChatHub> _hub;
        private readonly IList<Requisicao> _repository = new List<Requisicao>();
        public Worker(IHubContext<ChatHub> hub)
        {
            _hub = hub;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();

            var conf = new ConsumerConfig
            {
                GroupId = "test-consumer-group",
                BootstrapServers = "broker:29092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(conf).Build();
            consumer.Subscribe("fila_pedido");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var cr = consumer.Consume(stoppingToken);

                    Requisicao? requisicao = JsonSerializer.Deserialize<Requisicao>(cr.Value);
                    _repository.Add(requisicao);

                    await _hub.Clients.All.SendAsync("SendMessage", _repository);
                    Console.WriteLine($"executou: {DateTime.Now} | retorno: {cr.Value}");
                }
                catch (ConsumeException ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
        }
    }
}
