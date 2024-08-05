using Confluent.Kafka;
using ProvaConceitoSignalR.Model;
using System.Text.Json;

namespace ProvaConceitoSignalR.Kafka
{
    public class PubKafka
    {
        private readonly string _topic;

        public PubKafka()
        {
            _topic = "fila_pedido";
        }

        public async Task ExecutePub(Requisicao requisicao)
        {
            var conf = new ProducerConfig { BootstrapServers = "broker:29092" };

            using var producer = new ProducerBuilder<string, string>(conf).Build();
            var obj = JsonSerializer.Serialize(requisicao);

            await producer.ProduceAsync(_topic, new Message<string, string> { Value = obj });
        }
    }
}
