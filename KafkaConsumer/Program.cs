Console.Title = "Kafka Consumer";

Console.WriteLine("Kafka - comsumming");

new KafkaConsumer.Consumer().Consume();

Console.WriteLine("Kafka - done");
