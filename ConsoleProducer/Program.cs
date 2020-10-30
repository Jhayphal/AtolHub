using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProducer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var config = new ProducerConfig { BootstrapServers = "95.217.189.182:9092" };

            // If serializers are not specified, default serializers from
            // `Confluent.Kafka.Serializers` will be automatically used where
            // available. Note: by default strings are encoded as UTF8.
            using (var p = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var dr = await p.ProduceAsync("Jhayphal", new Message<Null, string> { Value = "test" });
                    Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
            }

            Console.ReadLine();
        }
    }

    public class KafkaQueue : BaseEntity
    {
        public string Header { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public string Topic { get; set; }
        public bool IsInError { get; set; }
        public bool IsSync { get; set; }
        public KafkaDirection Direction { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
    }

    public enum KafkaDirection
    {
        INCOMMINT = 10,
        OUTGOING = 20
    }
}
