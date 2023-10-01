using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using KafkaFlow;
using KafkaFlow.TypedHandler;
using UserService.Shared.Infrastructure.Bus.Kafka.Consumer.Handlers;
using UserService.Shared.Infrastructure.Bus.Kafka.Logs;
using UserService.Shared.Infrastructure.Bus.Kafka.Producer;

namespace UserService.Config.Bus.Kafka
{
    public class kafkaServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            var kafkaOptionss = configuration.GetSection(BusOptionsSetup.ConfigurationSectionName).Get<KafkaOptions>()!;
            const string usersTopic = "users";
            const string consumerName = "users-consumer";

            services.AddSingleton<KafkaProducer>();
            services.ConfigureOptions<BusOptionsSetup>();

            services.AddKafkaFlowHostedService(configuration =>
            {
                configuration.UseLogHandler<CustomLogHandler>();
                configuration.AddCluster(cluster =>
                {
                    cluster.WithBrokers(kafkaOptionss.BootstrapServers);
                    cluster.WithSchemaRegistry(config => config.Url = kafkaOptionss.SchemaRegistryUrl);
                    cluster.CreateTopicIfNotExists(usersTopic, 1, 1);

                    cluster.AddConsumer(consumer =>
                    {
                        consumer.Topics(kafkaOptionss.Topics);
                        consumer.WithName(consumerName);
                        consumer.WithGroupId(kafkaOptionss.GroupId);
                        consumer.WithBufferSize(100);
                        consumer.WithWorkersCount(3);
                        consumer.WithAutoOffsetReset(AutoOffsetReset.Earliest);
                        consumer.WithManualStoreOffsets();
                        consumer.AddMiddlewares(middlewares => middlewares
                            .AddSchemaRegistryAvroSerializer()
                            .AddTypedHandlers(handlers => handlers
                                // Transient needed because of mediatr(is transient by default)
                                .WithHandlerLifetime(InstanceLifetime.Transient)
                                .AddHandler<UserCreatedConsumerEventHandler>()
                            )
                        );

                    });

                    cluster.AddProducer<KafkaProducer>(producer =>
                    {
                        producer.DefaultTopic(usersTopic);
                        producer.WithAcks(Acks.All);
                        producer.AddMiddlewares(middlewares =>
                        {
                            middlewares.AddSchemaRegistryAvroSerializer(
                                new AvroSerializerConfig
                                {
                                    SubjectNameStrategy = SubjectNameStrategy.TopicRecord,
                                    AutoRegisterSchemas = true
                                }
                            );
                        });
                    });

                });
            });
        }
    }
}