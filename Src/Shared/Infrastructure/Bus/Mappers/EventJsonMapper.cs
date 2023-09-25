using System.Text.Json;
using System.Text.Json.Serialization;
using UserService.Shared.Infrastructure.Bus.Consumer.Core;
using UserService.Shared.Infrastructure.Bus.Consumer.Events;

namespace UserService.Shared.Infrastructure.Bus.Mappers
{
    public class EventJsonMapper : JsonConverter<IConsumerEvent>
    {
        public override IConsumerEvent? Read(ref Utf8JsonReader reader, System.Type typeToConvert, JsonSerializerOptions options)
        {
            if (!JsonDocument.TryParseValue(ref reader, out var doc))
            {
                throw new JsonException($"Failed to parse {nameof(JsonDocument)}");
            }

            if (!doc.RootElement.TryGetProperty("Type", out var type))
            {
                throw new JsonException("Could not detect the Type discriminator property!");
            }

            var typeValue = type.GetString();
            var json = doc.RootElement.GetRawText();

            // we only need to deserialize the event that we are interested in, otherwise we can ignore it
            return typeValue switch
            {
                UserCreatedConsumerEvent.EVENT_NAME => JsonSerializer.Deserialize<UserCreatedConsumerEvent>(json, options),
                _ => new IgnoreConsumerEvent()
            };
        }

        public override void Write(Utf8JsonWriter writer, IConsumerEvent value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}