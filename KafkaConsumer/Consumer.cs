namespace KafkaConsumer;

public class Consumer
{
    private ConsumerConfig _config;

    public Consumer()
    {
        _config = new ConsumerConfig
        {
            GroupId = "test-consumer-group",
            BootstrapServers = "localhost:9092",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
    }

    public void Consume()
    {
        using var consumer = new ConsumerBuilder<Ignore, string>(_config).Build();
        consumer.Subscribe("test-topic");

        try
        {
            while (true)
            {
                var consumeResult = consumer.Consume();
                var car = JsonSerializer.Deserialize<Car>(consumeResult.Message.Value);
                Console.WriteLine($"Received message: Id={car.Id}, Name={car.Name}, ManufacturingDate={car.ManufacturingDate.ToString("dd/MM/yyyy")}");
            }
        }
        catch (OperationCanceledException)
        {
            consumer.Close();
        }
    }
}
