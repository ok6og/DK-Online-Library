using Confluent.Kafka;
using MessagePack;

namespace BookStore.BL.Kafka
{
    internal class MsgPackDeserializer<TValue> : IDeserializer<TValue>
    {
        public TValue Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            return MessagePackSerializer.Deserialize<TValue>(data.ToArray());
        }
    }
}