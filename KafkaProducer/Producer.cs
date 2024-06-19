namespace KafkaProducer;

public class Producer
{
    private ProducerConfig _config;

    public Producer()
    {
        _config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092",
        };
    }

    public DeliveryResult<Null, string> Produce<T>(T message)
    {
        using var producer = new ProducerBuilder<Null, string>(_config).Build();

        var json = string.Empty;

        if(IsComplexType(message!))
            json = JsonSerializer.Serialize(message);

        DeliveryResult<Null, string> result = producer
                .ProduceAsync("test-topic", new Message<Null, string> { Value = json })
                .GetAwaiter()
                .GetResult();

        producer.Flush(TimeSpan.FromSeconds(10));

        return result;
    }

    private bool IsComplexType(object obj)
    {
        if (obj == null)
            return false;

        Type type = obj.GetType();
        
        return !(type.IsPrimitive ||
                 type == typeof(string) ||
                 type == typeof(DateTime) ||
                 type == typeof(decimal) ||
                 type.IsEnum);
    }
}
