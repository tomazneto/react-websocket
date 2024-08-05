using Confluent.Kafka;
using Microsoft.AspNetCore.SignalR;
using ProvaConceitoSignalR.Hub;
using System.Text.Json;

namespace ProvaConceitoSignalR.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IHubContext<ChatHub> _hub;
        private readonly IList<Teste> _repository = new List<Teste>();
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

                    //Teste? teste = JsonSerializer.Deserialize<Teste>(cr.Value);
                    //_repository.Add(teste);

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

    public class Teste
    {
        public int ano { get; set; }
        public int orderid { get; set; }
    }
}
