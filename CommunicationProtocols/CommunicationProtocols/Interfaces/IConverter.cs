namespace TrainsInfo.DataStream.Communication.Protocol.Dsccp.Interfaces
{
    interface IConverter
    {
        Message FromBytes(object data);
        byte[] ToBytes(Message message);
    }
}
