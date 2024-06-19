using Confluent.Kafka;

public class Program
{
    static void Main(string[] args)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092",
        };

        using var producer = new ProducerBuilder<Null, string>(config).Build();

        for (int i = 0; i < 10; i++)
        {
            var result = producer
                .ProduceAsync("test-topic", new Message<Null, string> { Value = $"Message {i}" })
                .GetAwaiter()
                .GetResult();

            Console.WriteLine($"Sent message to {result.TopicPartitionOffset}");
        }

        producer.Flush(TimeSpan.FromSeconds(10));
    }
}
