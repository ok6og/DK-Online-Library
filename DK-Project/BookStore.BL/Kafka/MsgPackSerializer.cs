using Confluent.Kafka;
using MessagePack;

namespace BookStore.BL.Kafka
{
    public class MsgPackSerializer<TValue> : ISerializer<TValue>
    {
        public byte[] Serialize(TValue data, SerializationContext context)
        {
            return MessagePackSerializer.Serialize(data);
        }
    }
}