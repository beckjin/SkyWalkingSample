using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;

namespace MD.MongoDB
{
    /// <summary>
    /// 自定义 Bson 序列化器
    /// </summary>
    public class BsonSerializationRepository : IBsonSerializationProvider
    {
        /// <summary>
        /// 获取序列化器
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IBsonSerializer GetSerializer(Type type)
        {
            if (type == typeof(int?)) return new NullableInt32Serializer();
            if (type == typeof(long?)) return new NullableInt64Serializer();
            else return null;
        }
    }

    class NullableInt32Serializer : SerializerBase<int?>
    {
        public override int? Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            if (context.Reader.CurrentBsonType == BsonType.Int32) { return context.Reader.ReadInt32(); }
            else if (context.Reader.CurrentBsonType == BsonType.Int64) { return (int)context.Reader.ReadInt64(); }
            else if (context.Reader.CurrentBsonType == BsonType.Double) { return (int)context.Reader.ReadDouble(); }
            else if (context.Reader.CurrentBsonType == BsonType.Null) { context.Reader.ReadNull(); }
            else if (context.Reader.CurrentBsonType == BsonType.Undefined) { context.Reader.ReadUndefined(); }
            return null;
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, int? value)
        {
            if (value != null) context.Writer.WriteInt32(value.Value);
            else context.Writer.WriteUndefined();
        }
    }

    class NullableInt64Serializer : SerializerBase<long?>
    {
        public override long? Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            if (context.Reader.CurrentBsonType == BsonType.Int32) { return context.Reader.ReadInt32(); }
            else if (context.Reader.CurrentBsonType == BsonType.Int64) { return context.Reader.ReadInt64(); }
            else if (context.Reader.CurrentBsonType == BsonType.Double) { return (long)context.Reader.ReadDouble(); }
            else if (context.Reader.CurrentBsonType == BsonType.Null) { context.Reader.ReadNull(); }
            else if (context.Reader.CurrentBsonType == BsonType.Undefined) { context.Reader.ReadUndefined(); }
            return null;
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, long? value)
        {
            if (value != null) context.Writer.WriteInt64(value.Value);
            else context.Writer.WriteUndefined();
        }
    }
}
